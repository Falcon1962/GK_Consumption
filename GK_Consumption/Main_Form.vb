Imports System.Globalization
Imports System.IO
Imports System.Windows.Forms
Imports System.Net.Security
Imports System.Text.RegularExpressions
Imports GK_Consumption.Main_Form
Imports System.Reflection
Imports Irony
Imports System.Xml.Schema
Imports GK_Consumption.XML
Imports System.Xml.Serialization

Public Class Main_Form

    Enum Shift
        NoShift = 0
        Morning = 1
        Afternoon = 2
        Night = 3
    End Enum

    Const NumIngredients As Integer = 30



    Public InstalledIngredients(NumIngredients) As Boolean
    'Public IngrUnit(NumIngredients) As String
    Public Consumption(3, NumIngredients) As Double
    Public TxtFileName As String
    Public LogPath As String
    Public NrOfShifts As Integer
    Public Shifts(3) As TimeOnly
    ' Dictionary to hold the machine configurations where the key is the machine number

    Public SelectedMachine As String

    Dim timeRegex As New Regex("^(2[0-3]|[01]?[0-9]):([0-5]?[0-9])$")





    Private Sub MonthCalendar_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar.DateChanged
        Selection_From.Text = MonthCalendar.SelectionRange.Start
        Selection_To.Text = MonthCalendar.SelectionRange.End
        Selection_Text.Text = "Selected period:"
        GetConsumption()
    End Sub

    Private Async Sub GetConsumption()
        Dim LogFileName As String
        Dim Selectiondate As DateOnly
        Dim Enddate As DateOnly
        Dim pos, pos2 As Integer
        Dim ErrorInLine As Boolean
        Dim line As String
        Dim DecPoint As String = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
        Dim IngrNr As Integer
        Dim Value As Double
        Dim CurrentShift As Shift

        For IngrNr = 0 To NumIngredients
            InstalledIngredients(IngrNr) = False
            For i As Shift = Shift.NoShift To Shift.Night
                Consumption(i, IngrNr) = 0
            Next
        Next

        Selectiondate = DateOnly.FromDateTime(MonthCalendar.SelectionRange.Start)
        Enddate = DateOnly.FromDateTime(MonthCalendar.SelectionRange.End)
        If CB_IncludeShifts.Checked Then
            If Selectiondate < DateOnly.FromDateTime(DateTime.Now) Then
                Enddate = Enddate.AddDays(1)
            Else
                Selectiondate = Selectiondate.AddDays(-1)
            End If
            Selection_From.Text = Selectiondate.ToString & " " & Shifts(Shift.Morning).ToString
            Selection_To.Text = Enddate.ToString & " " & Shifts(Shift.Morning).ToString
        Else
            Selection_From.Text = Selectiondate.ToString
            Selection_To.Text = Enddate.ToString
        End If

        'While DateDiff("d", Selectiondate, MonthCalendar.SelectionRange.End) >= 0
        While Selectiondate <= Enddate
            LogFileName = IO.Path.Combine(LogPath, Selectiondate.ToString("yyyyMMdd") & ".LOG")
            If IO.File.Exists(LogFileName) Then
                'Txt_Read.Text = "Reading " & LogFileName
                Await Task.Delay(10)
                Using Reader As StreamReader = New StreamReader(LogFileName)
                    While Not Reader.EndOfStream
                        line = Reader.ReadLine
                        Dim LineTime As TimeOnly = TimeOnly.ParseExact(Mid(line, 1, 6), "HHmmss", Nothing)
                        'Remove timestamp & MessID
                        pos = InStr(line, "#") ' search for MessID
                        If pos > 0 Then
                            pos2 = InStr(pos, line, ";") ' search for MessID end
                            If (pos2 - pos) <> 4 Then
                                'Invalid messageID
                                ErrorInLine = True
                            Else
                                'Remove time & MessID
                                line = Mid(line, pos2 + 1)
                            End If
                        Else
                            'No messageID, Remove time 
                            line = Mid(line, 7)
                        End If

                        Dim LineArray() As String = Split(line, ";")
                        Select Case LineArray(0)
                            Case "I", "P", "G", "L"
                                IngrNr = LineArray(2)
                                If Not Double.TryParse(LineArray(4).Replace(".", DecPoint), Value) Then
                                    Txt_Read.Text = "Could not parse " & LineArray(4).Replace(".", DecPoint)
                                    Await Task.Delay(100)
                                Else
                                    'add the value to the arrays
                                    InstalledIngredients(IngrNr) = True

                                    If CB_IncludeShifts.Checked Then
                                        CurrentShift = GetShift(LineTime, Selectiondate) 'Shift.Morning
                                        If CurrentShift <> Shift.NoShift Then
                                            Consumption(CurrentShift, IngrNr) += Value
                                            Consumption(Shift.NoShift, IngrNr) += Value
                                        End If
                                    Else
                                        Consumption(Shift.NoShift, IngrNr) += Value
                                    End If
                                End If
                        End Select
                    End While
                End Using
            End If
            Selectiondate = Selectiondate.AddDays(1)
        End While
        CreateConsumptionLines()

    End Sub


    Private Function ConsumptionText(IngrNr As Integer, Consumption As Double) As String
        Dim Val As Double = Consumption * MachineConfigurations(SelectedMachine).Ingredients(IngrNr - 1).Ingredient.ConversionFactor
        ConsumptionText = Val.ToString("F2") & " " & MachineConfigurations(SelectedMachine).Ingredients(IngrNr - 1).Ingredient.Unit
    End Function


    Private Sub CreateConsumptionLines()
        Dim ypos As Integer = 50
        Dim n As Integer = 0
        Dim Lbl As Label

        GB_Usage.Controls.Clear()

        ypos = 30
        'Add Name
        Lbl = New Label
        GB_Usage.Controls.Add(Lbl)
        With Lbl
            .Size = New Size(100, 15)
            .Location = New Point(50, ypos)
            .Name = "LblIngr"
            .Text = GK.TextLookup("IngrName", "Ingredient Name")
            .Font = New Font(Lbl.Font, FontStyle.Bold)
        End With

        'Add Name
        Lbl = New Label
        GB_Usage.Controls.Add(Lbl)
        With Lbl
            .Size = New Size(100, 15)
            .Location = New Point(150, ypos)
            .Name = "LblTotals"
            .Text = GK.TextLookup("Totals", "Totals")
            .TextAlign = ContentAlignment.MiddleRight
            .Font = New Font(Lbl.Font, FontStyle.Bold)
        End With

        If CB_IncludeShifts.Checked Then
            For i As Integer = 1 To NrOfShifts
                'Add Name
                Lbl = New Label
                GB_Usage.Controls.Add(Lbl)
                With Lbl
                    .Size = New Size(100, 15)
                    .Location = New Point(250 + (i - 1) * 100, ypos)
                    .TextAlign = ContentAlignment.MiddleRight
                    .Name = "LblShiftName_" & i.ToString
                    .Text = GK.TextLookup("Shift" & i.ToString, "Shift" & i.ToString) 'TxtShift(i)
                    .Font = New Font(Lbl.Font, FontStyle.Bold)
                End With

            Next
        End If

        For i As Integer = 1 To NumIngredients
            If InstalledIngredients(i) Then
                n += 1
                ypos = (n - 1) * 20 + 50
                'Add Name
                Lbl = New Label
                GB_Usage.Controls.Add(Lbl)
                With Lbl
                    .Size = New Size(100, 15)
                    .Location = New Point(50, ypos)
                    .Name = "LblName" & i.ToString
                    .Text = MachineConfigurations(SelectedMachine).Ingredients(i - 1).Ingredient.Name
                    'GK.TextLookup("Ingredient_" & i.ToString, "Ingredient_" & i.ToString)
                End With

                'Add Value
                Lbl = New Label
                GB_Usage.Controls.Add(Lbl)
                With Lbl
                    .AutoSize = False
                    .Size = New Size(100, 15)
                    .Location = New Point(150, ypos)
                    .Name = "LblTotal" & i.ToString
                    .Text = ConsumptionText(i, Consumption(0, i))
                    .TextAlign = ContentAlignment.MiddleRight
                End With

                If CB_IncludeShifts.Checked Then
                    For j As Integer = Shift.Morning To NrOfShifts
                        'Add Value
                        Lbl = New Label
                        GB_Usage.Controls.Add(Lbl)
                        With Lbl
                            .AutoSize = False
                            .Size = New Size(100, 15)
                            .Location = New Point(250 + (j - 1) * 100, ypos)
                            .Name = "LblTotal_" & j.ToString & i.ToString
                            .Text = ConsumptionText(i, Consumption(j, i))
                            .TextAlign = ContentAlignment.MiddleRight
                        End With
                    Next
                End If

            End If
        Next
    End Sub

    ''' <summary>
    ''' Read the textfile used for translation & customer specific texts
    ''' </summary>
    Private Sub ReadNames()
        If File.Exists(TxtFileName) Then
            GK.LanguageText = My.Computer.FileSystem.ReadAllText(TxtFileName, System.Text.Encoding.Unicode)
        Else
            GK.LanguageText = ""
            MessageBox.Show("Text file " & TxtFileName & " is not found.")
        End If
    End Sub


    ''' <summary>
    ''' Determines what shift the LineTime & LineDate belong to
    ''' </summary>
    ''' <param name="LineTime"></param>
    ''' <param name="LineDate"></param>
    ''' <returns></returns>
    Private Function GetShift(LineTime As TimeOnly, LineDate As DateOnly) As Shift
        Dim RetVal As Shift = Shift.NoShift

        If LineTime < Shifts(Shift.Morning) Then
            If LineDate = DateOnly.FromDateTime(MonthCalendar.SelectionRange.Start) Then
                'First day, first shift does not belong to any period
                RetVal = Shift.NoShift
            Else
                'last shift
                RetVal = NrOfShifts
            End If
        ElseIf LineTime < Shifts(Shift.Afternoon) Then
            If LineDate > DateOnly.FromDateTime(MonthCalendar.SelectionRange.End) Then
                RetVal = Shift.NoShift
            Else
                RetVal = Shift.Morning
            End If

        ElseIf LineTime < Shifts(Shift.Night) Then
            If LineDate > DateOnly.FromDateTime(MonthCalendar.SelectionRange.End) Then
                RetVal = Shift.NoShift
            Else
                RetVal = Shift.Afternoon
            End If
        Else
            If LineDate > DateOnly.FromDateTime(MonthCalendar.SelectionRange.End) Then
                RetVal = Shift.NoShift
            Else
                RetVal = Shift.Night
            End If
        End If
        If RetVal > NrOfShifts Then
            RetVal = NrOfShifts
        End If
        GetShift = RetVal

    End Function



    ''' <summary>
    ''' Check if enviroment variables exist for the current user and open settings if not; now obsolete
    ''' </summary>
    Private Sub CheckEnvVar()
        LogPath = Environment.GetEnvironmentVariable("GK_Usage_LogPath", EnvironmentVariableTarget.User)
        If String.IsNullOrEmpty(LogPath) Then
            ' If environment variable doesn't exist, show FolderBrowserDialog
            ChangePath()
        Else
            ' If it exists, display the current value
            TB_LogFilePath.Text = LogPath
        End If
        TxtFileName = Environment.GetEnvironmentVariable("GK_Usage_TxtFile", EnvironmentVariableTarget.User)
        If String.IsNullOrEmpty(TxtFileName) Then
            ' If environment variable doesn't exist, show FileOpenDialog
            ChangeTxtFile()
        Else
            ' If it exists, display the current value
            TB_TextFileName.Text = TxtFileName
        End If

        NrOfShifts = Environment.GetEnvironmentVariable("GK_Usage_NrOfShifts", EnvironmentVariableTarget.User)
        If String.IsNullOrEmpty(NrOfShifts) Then
            ' If environment variable doesn't exist, open settings
            OpenSettings()
        Else
            ' If it exists, check the correct radiobutton
            If NrOfShifts = 3 Then
                RbShift3.Checked = True
            ElseIf NrOfShifts = 2 Then
                RbShift2.Checked = True
            Else
                RbShift1.Checked = True
            End If
            ShowVisibleItems()
        End If
        For i As Integer = Shift.Morning To Shift.Night
            Dim Var As String = "GK_Usage_Shift" & i & "Start"
            Dim TimeS As String = Environment.GetEnvironmentVariable(Var, EnvironmentVariableTarget.User)
            If String.IsNullOrEmpty(TimeS) Then
                Select Case i
                    Case Shift.Morning
                        Shifts(i) = New TimeOnly(6, 0, 0)
                    Case Shift.Afternoon
                        Shifts(i) = New TimeOnly(14, 0, 0)
                    Case Shift.Night
                        Shifts(i) = New TimeOnly(22, 0, 0)
                End Select
                Environment.SetEnvironmentVariable(Var, Shifts(i).ToString, EnvironmentVariableTarget.User)
            Else
                Shifts(i) = New TimeOnly(Mid(TimeS, 1, 2), Mid(TimeS, 4, 2), 0)
            End If
            Dim _textBox As TextBox = CType(GB_Settings.Controls("TB_Shift" & i & "Start"), TextBox)
            If _textBox IsNot Nothing Then
                _textBox.Text = Shifts(i).ToString()
            End If
        Next


    End Sub

    ''' <summary>
    ''' Things to do when form is loaded
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Main_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Assembly As Assembly = Assembly.GetExecutingAssembly()
        Dim AssemblyName As AssemblyName = Assembly.GetName()
        Me.Text = AssemblyName.Name.ToString & " " & AssemblyName.Version.ToString

        Me.KeyPreview = True

        Txt_Read.Text = ""

        'CheckEnvVar()

        XML.LoadMachineConfigurationsFromFile()
        'ReadNames()
        ReloadMachines()
        'Dim test As String = ""
    End Sub


    Private Sub ReloadMachines()
        CbMachSelector.Items.Clear()
        For Each Config As MachineConfiguration In XML.MachineConfigurations.Values
            CbMachSelector.Items.Add(Config.MachineNumber)
        Next
        CbMachSelector.Visible = (CbMachSelector.Items.Count > 1)
        If CbMachSelector.Items.Count > 0 Then
            CbMachSelector.SelectedIndex = 0
        End If
        ' If it exists, check the correct radiobutton

        If NrOfShifts > 1 Then
            CB_IncludeShifts.Checked = True
        End If

    End Sub


    Private Sub CbMachSelector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbMachSelector.SelectedIndexChanged
        DisplaySettingsOnForm()
        GetConsumption()
    End Sub



    Private Sub DisplaySettingsOnForm()
        For Each Config As MachineConfiguration In XML.MachineConfigurations.Values
            If Config.MachineNumber = (CbMachSelector.SelectedItem) Then
                GB_Usage.Text = "Consumption " & Config.Settings.CustomerName
                TbMachNr.Text = Config.MachineNumber
                SelectedMachine = TbMachNr.Text
                TbMachName.Text = Config.Settings.CustomerName
                LogPath = Config.Settings.LogDir
                TB_LogFilePath.Text = LogPath
                TxtFileName = Config.Settings.TextFileName
                ReadNames()
                TB_TextFileName.Text = TxtFileName
                NrOfShifts = Config.Settings.NrOfShifts
                If Config.Settings.NrOfShifts = Nothing Then
                    NrOfShifts = 3
                Else
                    NrOfShifts = Config.Settings.NrOfShifts
                End If
                For i As Integer = Shift.Morning To Shift.Night
                    Dim TimeS As TimeOnly = Config.Settings.Shifts(i - 1)
                    If TimeS = Nothing Then
                        Select Case i
                            Case Shift.Morning
                                Shifts(i) = New TimeOnly(6, 0, 0)
                            Case Shift.Afternoon
                                Shifts(i) = New TimeOnly(14, 0, 0)
                            Case Shift.Night
                                Shifts(i) = New TimeOnly(22, 0, 0)
                        End Select
                    Else
                        Shifts(i) = TimeS
                    End If
                    If NrOfShifts = 3 Then
                        RbShift3.Checked = True
                    ElseIf NrOfShifts = 2 Then
                        RbShift2.Checked = True
                    Else
                        RbShift1.Checked = True
                    End If
                    ShowVisibleItems()

                    Dim _textBox As TextBox = CType(GB_Settings.Controls("TB_Shift" & i & "Start"), TextBox)
                    If _textBox IsNot Nothing Then
                        _textBox.Text = Shifts(i).ToString()
                    End If
                Next

            End If
        Next
    End Sub



    ''' <summary>
    ''' Handle Ctrl-C to copy the consumption data to the clipboard
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Main_Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.C Then
            ' Handle Ctrl + C
            CopyConsumption()
            ' Prevent further processing of this key combination
            e.SuppressKeyPress = True
        End If
    End Sub


    Private Sub CB_IncludeShifts_CheckedChanged(sender As Object, e As EventArgs) Handles CB_IncludeShifts.CheckedChanged
        GetConsumption()
    End Sub

    Private Sub BtClearSettings_Click(sender As Object, e As EventArgs) Handles BtClearSettings.Click
        SelectedMachine = TbMachNr.Text
        Dim confirmDialog As New FormConfirmDialog()
        confirmDialog.Question = "Are you sure you want to remove all settings" & Environment.NewLine & " for the machine " & SelectedMachine & "?"
        confirmDialog.StartPosition = FormStartPosition.CenterParent
        Dim result As DialogResult = confirmDialog.ShowDialog(Me)
        If result = DialogResult.Yes Then
            If XML.MachineConfigurations.ContainsKey(TbMachNr.Text) Then
                XML.MachineConfigurations.Remove(TbMachNr.Text)
            End If
            SaveMachineConfigurationsToFile()
            ReloadMachines()
            If CbMachSelector.Items.Count > 0 Then
                TbMachNr.Text = CbMachSelector.Items(CbMachSelector.SelectedIndex)
                CbMachSelector.SelectedIndex = CbMachSelector.Items.Count - 1
            End If

            For Each TB As TextBox In GB_Settings.Controls.OfType(Of TextBox)()
                TB.Text = ""
            Next
        End If
        CloseSettings()
    End Sub


    Private Sub RemoveEnvSettingsOldProgram()
        Environment.SetEnvironmentVariable("GK_Usage_LogPath", Nothing, EnvironmentVariableTarget.User)
        Environment.SetEnvironmentVariable("GK_Usage_TxtFile", Nothing, EnvironmentVariableTarget.User)
        Environment.SetEnvironmentVariable("GK_Usage_NrOfShifts", Nothing, EnvironmentVariableTarget.User)
        Environment.SetEnvironmentVariable("GK_Usage_Shift1Start", Nothing, EnvironmentVariableTarget.User)
        Environment.SetEnvironmentVariable("GK_Usage_Shift2Start", Nothing, EnvironmentVariableTarget.User)
        Environment.SetEnvironmentVariable("GK_Usage_Shift3Start", Nothing, EnvironmentVariableTarget.User)
    End Sub


    Private Sub GB_Settings_Enter(sender As Object, e As EventArgs) Handles GB_Settings.Enter
        TB_LogFilePath.Text = LogPath
    End Sub

    Private Sub BtSettings_Click(sender As Object, e As EventArgs) Handles BtSettings.Click
        DisplaySettingsOnForm()
        OpenSettings()
    End Sub

    Public Sub OpenSettings()
        GB_Usage.Visible = False
        GB_Settings.Visible = True
        BtSettings.Visible = False
    End Sub


    Private Sub BtCloseSett_Click(sender As Object, e As EventArgs) Handles BtCloseSett.Click
        CloseSettings()
    End Sub

    Private Sub CloseSettings()
        GB_Settings.Visible = False
        GB_Usage.Visible = True
        BtSettings.Visible = True
        If Not File.Exists(TxtFileName) Then
            ChangeTxtFile()
        End If
        If Not Directory.Exists(LogPath) Then
            ChangePath()
        End If
    End Sub


    Private Sub BtChangePath_Click(sender As Object, e As EventArgs) Handles BtChangePath.Click
        ChangePath()
    End Sub

    Private Sub ChangePath()
        Using folderBrowser As New FolderBrowserDialog()
            folderBrowser.Description = "Select a folder to set as ProgPath"
            folderBrowser.ShowNewFolderButton = True

            ' Show the dialog and check if the user selected a folder
            If folderBrowser.ShowDialog() = DialogResult.OK Then
                ' Get the selected path
                LogPath = folderBrowser.SelectedPath
                TB_LogFilePath.Text = LogPath
                Environment.SetEnvironmentVariable("GK_Usage_LogPath", LogPath, EnvironmentVariableTarget.User)
            End If
        End Using
    End Sub

    Private Sub BtChangeTextFile_Click(sender As Object, e As EventArgs) Handles BtChangeTextFile.Click
        ChangeTxtFile()
    End Sub

    Private Sub ChangeTxtFile()
        Using fileBrowser As New OpenFileDialog
            fileBrowser.Title = "Select the text file to use."
            fileBrowser.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            If fileBrowser.ShowDialog() = DialogResult.OK Then
                TxtFileName = fileBrowser.FileName
                TB_TextFileName.Text = TxtFileName
                Environment.SetEnvironmentVariable("GK_Usage_TxtFile", TxtFileName, EnvironmentVariableTarget.User)
            End If
        End Using

    End Sub

    Private Sub TB_LogFilePath_KeyPressed(sender As Object, e As KeyPressEventArgs) Handles TB_LogFilePath.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            LogPath = TB_LogFilePath.Text
            Environment.SetEnvironmentVariable("GK_Usage_LogPath", LogPath, EnvironmentVariableTarget.User)
        End If
    End Sub

    Private Sub TB_TextFileName_KeyPressed(sender As Object, e As KeyPressEventArgs) Handles TB_TextFileName.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            TxtFileName = TB_TextFileName.Text
            Environment.SetEnvironmentVariable("GK_Usage_TxtFile", TxtFileName, EnvironmentVariableTarget.User)
        End If
    End Sub



    Private Sub RbShift_Clicked(sender As Object, e As EventArgs) Handles RbShift1.Click, RbShift2.Click, RbShift3.Click
        Dim Rb As RadioButton = CType(sender, RadioButton)

        If Rb.Checked Then
            NrOfShifts = GetFirstIntegerFromText(Rb.Text)
            ShowVisibleItems()
            Environment.SetEnvironmentVariable("GK_Usage_NrOfShifts", NrOfShifts, EnvironmentVariableTarget.User)
            'MessageBox.Show(NrOfShifts.ToString)
        End If
    End Sub

    Private Sub ShowVisibleItems()
        LblShift3Start.Visible = (NrOfShifts > 2)
        TB_Shift3Start.Visible = (NrOfShifts > 2)
        LblShift2Start.Visible = (NrOfShifts > 1)
        TB_Shift2Start.Visible = (NrOfShifts > 1)
        LblShift1Start.Visible = (NrOfShifts > 1)
        TB_Shift1Start.Visible = (NrOfShifts > 1)
        CB_IncludeShifts.Visible = (NrOfShifts > 1)

    End Sub



    Private Sub BtCopyConsumption_Click(sender As Object, e As EventArgs) Handles BtCopyConsumption.Click
        CopyConsumption()
    End Sub



    ''' <summary>
    ''' Copy the consumption data to the clipboard
    ''' Invoked by BtCopyConsumption.Click & Cntr-C
    ''' </summary>
    Private Sub CopyConsumption()
        Dim s As String = GK.TextLookup("TotalsReport", "Consumption report") & " " & TbMachName.Text & vbCrLf
        s &= "From " & Selection_From.Text & " to " & Selection_To.Text & vbCrLf
        s &= CreateConsumptionDataString()
        Clipboard.SetText(s)
    End Sub



    ''' <summary>
    ''' Returns a string with all consumption data displayed on screen
    ''' </summary>
    ''' <returns></returns>
    Private Function CreateConsumptionDataString() As String
        Dim s As String = ""
        Dim lastTop As Integer = Integer.MinValue ' Keep track of the previous Top value

        ' Sort the controls by their Top property to ensure we process them in vertical order
        Dim labels = GB_Usage.Controls.OfType(Of Label)().OrderBy(Function(lbl) lbl.Top)

        For Each item As Label In labels
            ' Check if the current label is positioned lower than the previous one
            If item.Top > lastTop Then
                ' If it's a new row, add a new line (except for the first iteration)
                If lastTop <> Integer.MinValue Then
                    s = s.TrimEnd(vbTab) ' Remove the trailing tab from the previous line
                    s &= vbCrLf
                End If
            End If

            ' Add the text to the string with a tab separator
            s &= item.Text & vbTab

            ' Update the lastTop value to the current label's Top position
            lastTop = item.Top
        Next
        Return s.TrimEnd(vbTab) ' Remove the trailing tab from the last line

    End Function



#Region "Validating"
    Private Sub TextBoxTime_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_Shift1Start.Validating, TB_Shift2Start.Validating, TB_Shift3Start.Validating
        Dim inputTime As String = sender.Text
        Dim _textBox As TextBox = CType(sender, TextBox)
        Dim ShiftNr As Integer = GetFirstIntegerFromText(_textBox.Name)

        ' Check if the input matches the HH:mm format using the regular expression
        If Not timeRegex.IsMatch(inputTime) Then
            If inputTime = "" Then
                MessageBox.Show(ShiftNr.ToString & " does not exist " & _textBox.Name, "Input not OK", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                'MessageBox.Show("Invalid time format. Please enter a time in HH:mm format.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                _textBox.ForeColor = Color.Red
                e.Cancel = True
            End If
        Else
            _textBox.ForeColor = Color.Black
            'Environment.SetEnvironmentVariable("GK_Usage_Shift" & ShiftNr & "Start", Shifts(ShiftNr).ToString, EnvironmentVariableTarget.User)
        End If
    End Sub



    Private Sub TB_LogFilePath_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_LogFilePath.Validating
        Dim _textBox As TextBox = CType(sender, TextBox)

        If Not Directory.Exists(_textBox.Text) Then
            'MessageBox.Show("Folder " & _textBox.text & " does not exist.", "Setting error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _textBox.ForeColor = Color.Red
            e.Cancel = True
        Else
            _textBox.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TB_TextFileName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TB_TextFileName.Validating
        Dim _textBox As TextBox = CType(sender, TextBox)
        If Not File.Exists(_textBox.Text) Then
            'MessageBox.Show("File " & _textBox.Text & " does not exist.", "Setting error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _textBox.ForeColor = Color.Red
            'e.Cancel = True
        Else
            _textBox.ForeColor = Color.Black
        End If
    End Sub


    Private Function GetFirstIntegerFromText(input As String) As Integer
        Dim regex As New Regex("\d+")

        ' Find the first match of a number in the input string
        Dim match As Match = regex.Match(input)
        If match.Success Then
            ' Convert the matched value to an integer and return it
            Return Integer.Parse(match.Value)
        Else
            ' Return Nothing if no integer was found
            Return Nothing
        End If
    End Function


    Private Sub BtCreateExcel_Click(sender As Object, e As EventArgs) Handles BtCreateExcel.Click
        Dim s As String = CreateConsumptionDataString()
        ExcelTools.GenerateConsumptionReport(Selection_From.Text, Selection_To.Text, s)
    End Sub

    Private Sub BtSaveSettings_Click(sender As Object, e As EventArgs) Handles BtSaveSettings.Click

        If TbMachNr.Text = "" Then
            TbMachNr.Select()
            MessageBox.Show("Please enter the machine number in its field.")
        Else
            If Not XML.MachineConfigurations.ContainsKey(TbMachNr.Text) Then
                Dim defaultSettings As New Settings() ' Initialize a new Settings object (or use an existing one)
                Dim defaultIngredients As New List(Of SerializableIngredient)() ' Initialize an empty list for Ingredients

                TxtFileName = TB_TextFileName.Text
                ReadNames()

                ' Add default ingredients
                For i As Integer = 1 To 30
                    Dim newIngredient As New Ingredient(GK.TextLookup("Ingredient_" & i.ToString, "Ingredient_" & i.ToString), If(i > 20, "s", "kg"), 1.0)
                    defaultIngredients.Add(New SerializableIngredient(i, newIngredient))
                Next

                ' Create a new MachineConfiguration object with the necessary values
                Dim newConfig As New MachineConfiguration(TbMachNr.Text, defaultSettings, defaultIngredients)
                newConfig.MachineNumber = TbMachNr.Text
                XML.MachineConfigurations.Add(newConfig.MachineNumber, newConfig)

            End If
            XML.MachineConfigurations(TbMachNr.Text).MachineNumber = TbMachNr.Text
            XML.MachineConfigurations(TbMachNr.Text).Settings.CustomerName = TbMachName.Text
            LogPath = TB_LogFilePath.Text
            XML.MachineConfigurations(TbMachNr.Text).Settings.LogDir = LogPath
            TxtFileName = TB_TextFileName.Text
            XML.MachineConfigurations(TbMachNr.Text).Settings.TextFileName = TxtFileName
            XML.MachineConfigurations(TbMachNr.Text).Settings.NrOfShifts = NrOfShifts

            For i As Integer = Shift.Morning To Shift.Night
                Dim _textBox As TextBox = CType(GB_Settings.Controls("TB_Shift" & i & "Start"), TextBox)
                Dim TimeS As String = _textBox.Text 'Shifts(i)

                If _textBox.Text <> "" Then
                    XML.MachineConfigurations(TbMachNr.Text).Settings.ShiftTimes(i - 1) = _textBox.Text
                Else
                    'XML.MachineConfigurations(TbMachNr.Text).Settings.ShiftTimes = New String() {"06:00", "14:00", "22:00"}
                    Select Case i
                        Case Shift.Morning
                            XML.MachineConfigurations(TbMachNr.Text).Settings.ShiftTimes(i - 1) = "06:00"
                            'Shifts(i) = New TimeOnly(6, 0, 0)
                        Case Shift.Afternoon
                            XML.MachineConfigurations(TbMachNr.Text).Settings.ShiftTimes(i - 1) = "14:00"
                           'Shifts(i) = New TimeOnly(14, 0, 0)
                        Case Shift.Night
                            XML.MachineConfigurations(TbMachNr.Text).Settings.ShiftTimes(i - 1) = "22:00"
                            'Shifts(i) = New TimeOnly(22, 0, 0)
                    End Select
                    _textBox.Text = Shifts(i).ToString
                    'XML.MachineConfigurations(TbMachNr.Text).Settings.Shifts(i - 1) = Shifts(i)
                End If
            Next

            SelectedMachine = TbMachNr.Text
            SaveMachineConfigurationsToFile()
            ReloadMachines()
            CbMachSelector.SelectedIndex = CbMachSelector.Items.Count - 1
            CloseSettings()
        End If
    End Sub




    Private Sub BtEditIngr_Click(sender As Object, e As EventArgs) Handles BtEditIngr.Click
        Ingredients.Show()
    End Sub



#End Region





End Class







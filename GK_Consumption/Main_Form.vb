Imports System.Globalization
Imports System.IO
Imports System.Net.Security

Public Class Main_Form

    Const NumIngredients As Integer = 30

    Public InstalledIngredients(NumIngredients) As Boolean
    Public Period_Consumption(NumIngredients) As Double
    Public LogDir As String = "C:\GK\GK295\LOG\"
    Public TxtFileName As String = "C:\GK\GK.TXT"
    Public IngrNames(NumIngredients) As String
    Public Offset As Integer = 0


    Private Sub MonthCalendar_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar.DateChanged
        Selection_From.Text = MonthCalendar.SelectionRange.Start
        Selection_To.Text = MonthCalendar.SelectionRange.End
        Selection_Text.Text = "Selected period:"
        GetConsumption()
    End Sub

    Private Async Sub GetConsumption()
        Dim LogFileName As String
        Dim Selectiondate As Date
        Dim pos, pos2 As Integer
        Dim ErrorInLine As Boolean
        Dim line As String
        Dim DecPoint As String = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
        Dim IngrNr As Integer
        Dim Value As Double
        For IngrNr = 0 To NumIngredients
            Period_Consumption(IngrNr) = 0
            InstalledIngredients(IngrNr) = False
        Next

        Selectiondate = MonthCalendar.SelectionRange.Start
        While DateDiff("d", Selectiondate, MonthCalendar.SelectionRange.End) >= 0
            LogFileName = IO.Path.Combine(LogDir, Selectiondate.ToString("yyyyMMdd") & ".LOG")
            If IO.File.Exists(LogFileName) Then
                Txt_Read.Text = "Reading " & LogFileName
                Await Task.Delay(100)
                Using Reader As StreamReader = New StreamReader(LogFileName)
                    While Not Reader.EndOfStream

                        line = Reader.ReadLine
                        'First remove timestamp & MessID
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
                        If LineArray(0) = "I" Or LineArray(0) = "P" Or LineArray(0) = "G" Then
                            IngrNr = LineArray(2)
                            If Not Double.TryParse(LineArray(4).Replace(".", DecPoint), Value) Then
                                Txt_Read.Text = "Could not parse " & LineArray(4).Replace(".", DecPoint)
                                Await Task.Delay(100)
                            Else
                                'add the value to the array
                                Period_Consumption(IngrNr) = Period_Consumption(IngrNr) + Value
                                InstalledIngredients(IngrNr) = True
                                'Txt_Read.Text = Value.ToString
                                If IngrNr = 2 Then
                                    Txt_Read.Text = Period_Consumption(IngrNr).ToString
                                End If
                            End If
                        End If
                    End While

                End Using

            End If

            Selectiondate = Selectiondate.AddDays(1)
        End While
        Txt_Read.Text = Period_Consumption(2).ToString
        CreateConsumptionLines()

    End Sub

    Private Sub CreateConsumptionLines()
        Dim ypos As Integer = 50
        Dim n As Integer = 0
        Dim Lbl As Label

        'DisposeUsage()
        GB_Usage.Controls.Clear()

        'Offset += 10
        For i As Integer = 1 To NumIngredients
            If InstalledIngredients(i) Then
                n += 1
                ypos = (n - 1) * 20 + 50 '+ Offset
                'Add Name
                Lbl = New Label
                GB_Usage.Controls.Add(Lbl)
                With Lbl
                    .Size = New Size(100, 15)
                    .Location = New Point(50, ypos)
                    .Name = "Lbl" & i.ToString
                    .Text = IngrNames(i)
                End With

                'Add Value
                Lbl = New Label
                GB_Usage.Controls.Add(Lbl)
                With Lbl
                    .AutoSize = False
                    .Size = New Size(100, 15)
                    .Location = New Point(150, ypos)
                    .Name = "Lbl" & i.ToString
                    .Text = Period_Consumption(i).ToString("F2")
                    .TextAlign = ContentAlignment.MiddleRight
                End With

            End If
        Next
    End Sub


    Private Sub ReadIngrNames()
        Dim Line As String
        Dim n As Integer

        For i As Integer = 0 To NumIngredients
            IngrNames(i) = ""
        Next
        Using Reader As StreamReader = New StreamReader(TxtFileName)
            While Not Reader.EndOfStream
                Line = Reader.ReadLine
                Dim LineArray() As String = Split(Line, "=")
                If Len(Trim(LineArray(0))) > 11 Then
                    If Mid(Trim(LineArray(0)), 1, 11) = "Ingredient_" Then
                        If Integer.TryParse(Mid(Trim(LineArray(0)), 12), n) Then
                            IngrNames(n) = LineArray(1)
                        End If
                    End If
                End If
                'IngrNames(NumIngredients) = ""
            End While
        End Using

    End Sub

    Private Async Sub DisposeUsage()
        'For Each item In Me.GB_Usage.Controls '.OfType(Of Label)()
        '    item.Dispose()
        'Next
    End Sub



    Private Sub Main_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisposeUsage()
        ReadIngrNames()
    End Sub
End Class

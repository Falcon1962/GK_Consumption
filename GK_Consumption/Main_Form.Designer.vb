<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main_Form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main_Form))
        MonthCalendar = New MonthCalendar()
        Selection_Text = New Label()
        Selection_From = New Label()
        Selection_To = New Label()
        Txt_Read = New Label()
        GB_Usage = New GroupBox()
        GB_Settings = New GroupBox()
        BtEditIngr = New Button()
        TbMachName = New TextBox()
        LblMachName = New Label()
        TbMachNr = New TextBox()
        LblMachNr = New Label()
        BtSaveSettings = New Button()
        RbShift3 = New RadioButton()
        RbShift2 = New RadioButton()
        LblNrOfShifts = New Label()
        RbShift1 = New RadioButton()
        TB_Shift3Start = New TextBox()
        LblShift3Start = New Label()
        TB_Shift2Start = New TextBox()
        LblShift2Start = New Label()
        TB_Shift1Start = New TextBox()
        LblShift1Start = New Label()
        BtChangeTextFile = New Button()
        BtChangePath = New Button()
        TB_TextFileName = New TextBox()
        LblTextFileName = New Label()
        BtCloseSett = New Button()
        TB_LogFilePath = New TextBox()
        Lbl_LogFilePath = New Label()
        BtClearSettings = New Button()
        CB_IncludeShifts = New CheckBox()
        BtSettings = New Button()
        BtCopyConsumption = New Button()
        BtCreateExcel = New Button()
        CbMachSelector = New ComboBox()
        GB_Settings.SuspendLayout()
        SuspendLayout()
        ' 
        ' MonthCalendar
        ' 
        MonthCalendar.Location = New Point(18, 63)
        MonthCalendar.MaxSelectionCount = 366
        MonthCalendar.Name = "MonthCalendar"
        MonthCalendar.TabIndex = 0
        ' 
        ' Selection_Text
        ' 
        Selection_Text.AutoSize = True
        Selection_Text.Location = New Point(18, 9)
        Selection_Text.Name = "Selection_Text"
        Selection_Text.Size = New Size(204, 15)
        Selection_Text.TabIndex = 1
        Selection_Text.Text = "Please select period with the calender"
        ' 
        ' Selection_From
        ' 
        Selection_From.AutoSize = True
        Selection_From.Location = New Point(18, 24)
        Selection_From.Name = "Selection_From"
        Selection_From.Size = New Size(12, 15)
        Selection_From.TabIndex = 2
        Selection_From.Text = "-"
        ' 
        ' Selection_To
        ' 
        Selection_To.AutoSize = True
        Selection_To.Location = New Point(18, 39)
        Selection_To.Name = "Selection_To"
        Selection_To.Size = New Size(12, 15)
        Selection_To.TabIndex = 3
        Selection_To.Text = "-"
        ' 
        ' Txt_Read
        ' 
        Txt_Read.AutoSize = True
        Txt_Read.Location = New Point(18, 434)
        Txt_Read.Name = "Txt_Read"
        Txt_Read.Size = New Size(12, 15)
        Txt_Read.TabIndex = 4
        Txt_Read.Text = "_"
        ' 
        ' GB_Usage
        ' 
        GB_Usage.Location = New Point(267, 48)
        GB_Usage.Name = "GB_Usage"
        GB_Usage.Size = New Size(585, 410)
        GB_Usage.TabIndex = 5
        GB_Usage.TabStop = False
        GB_Usage.Text = "Consumption"
        ' 
        ' GB_Settings
        ' 
        GB_Settings.Controls.Add(BtEditIngr)
        GB_Settings.Controls.Add(TbMachName)
        GB_Settings.Controls.Add(LblMachName)
        GB_Settings.Controls.Add(TbMachNr)
        GB_Settings.Controls.Add(LblMachNr)
        GB_Settings.Controls.Add(BtSaveSettings)
        GB_Settings.Controls.Add(RbShift3)
        GB_Settings.Controls.Add(RbShift2)
        GB_Settings.Controls.Add(LblNrOfShifts)
        GB_Settings.Controls.Add(RbShift1)
        GB_Settings.Controls.Add(TB_Shift3Start)
        GB_Settings.Controls.Add(LblShift3Start)
        GB_Settings.Controls.Add(TB_Shift2Start)
        GB_Settings.Controls.Add(LblShift2Start)
        GB_Settings.Controls.Add(TB_Shift1Start)
        GB_Settings.Controls.Add(LblShift1Start)
        GB_Settings.Controls.Add(BtChangeTextFile)
        GB_Settings.Controls.Add(BtChangePath)
        GB_Settings.Controls.Add(TB_TextFileName)
        GB_Settings.Controls.Add(LblTextFileName)
        GB_Settings.Controls.Add(BtCloseSett)
        GB_Settings.Controls.Add(TB_LogFilePath)
        GB_Settings.Controls.Add(Lbl_LogFilePath)
        GB_Settings.Controls.Add(BtClearSettings)
        GB_Settings.Location = New Point(226, 66)
        GB_Settings.Name = "GB_Settings"
        GB_Settings.Size = New Size(585, 349)
        GB_Settings.TabIndex = 6
        GB_Settings.TabStop = False
        GB_Settings.Text = "Settings"
        GB_Settings.Visible = False
        ' 
        ' BtEditIngr
        ' 
        BtEditIngr.Location = New Point(413, 153)
        BtEditIngr.Name = "BtEditIngr"
        BtEditIngr.Size = New Size(75, 42)
        BtEditIngr.TabIndex = 23
        BtEditIngr.Text = "Edit Ingredients"
        BtEditIngr.UseVisualStyleBackColor = True
        ' 
        ' TbMachName
        ' 
        TbMachName.Location = New Point(113, 54)
        TbMachName.Name = "TbMachName"
        TbMachName.Size = New Size(376, 23)
        TbMachName.TabIndex = 22
        ' 
        ' LblMachName
        ' 
        LblMachName.Location = New Point(6, 57)
        LblMachName.Name = "LblMachName"
        LblMachName.Size = New Size(100, 15)
        LblMachName.TabIndex = 21
        LblMachName.Text = "Machine name: "
        LblMachName.TextAlign = ContentAlignment.TopRight
        ' 
        ' TbMachNr
        ' 
        TbMachNr.Location = New Point(113, 24)
        TbMachNr.Name = "TbMachNr"
        TbMachNr.Size = New Size(105, 23)
        TbMachNr.TabIndex = 20
        ' 
        ' LblMachNr
        ' 
        LblMachNr.Location = New Point(6, 27)
        LblMachNr.Name = "LblMachNr"
        LblMachNr.Size = New Size(100, 15)
        LblMachNr.TabIndex = 19
        LblMachNr.Text = "Machinenr.: "
        LblMachNr.TextAlign = ContentAlignment.TopRight
        ' 
        ' BtSaveSettings
        ' 
        BtSaveSettings.BackgroundImage = My.Resources.Resources.Save
        BtSaveSettings.BackgroundImageLayout = ImageLayout.Stretch
        BtSaveSettings.Location = New Point(499, 7)
        BtSaveSettings.Name = "BtSaveSettings"
        BtSaveSettings.Size = New Size(40, 40)
        BtSaveSettings.TabIndex = 18
        BtSaveSettings.UseVisualStyleBackColor = True
        ' 
        ' RbShift3
        ' 
        RbShift3.AutoSize = True
        RbShift3.Location = New Point(187, 153)
        RbShift3.Name = "RbShift3"
        RbShift3.Size = New Size(31, 19)
        RbShift3.TabIndex = 17
        RbShift3.TabStop = True
        RbShift3.Text = "3"
        RbShift3.UseVisualStyleBackColor = True
        ' 
        ' RbShift2
        ' 
        RbShift2.AutoSize = True
        RbShift2.Location = New Point(150, 153)
        RbShift2.Name = "RbShift2"
        RbShift2.Size = New Size(31, 19)
        RbShift2.TabIndex = 16
        RbShift2.TabStop = True
        RbShift2.Text = "2"
        RbShift2.UseVisualStyleBackColor = True
        ' 
        ' LblNrOfShifts
        ' 
        LblNrOfShifts.Location = New Point(6, 155)
        LblNrOfShifts.Name = "LblNrOfShifts"
        LblNrOfShifts.Size = New Size(100, 15)
        LblNrOfShifts.TabIndex = 15
        LblNrOfShifts.Text = "Nr. of shifts:"
        LblNrOfShifts.TextAlign = ContentAlignment.TopRight
        ' 
        ' RbShift1
        ' 
        RbShift1.AutoSize = True
        RbShift1.Location = New Point(113, 153)
        RbShift1.Name = "RbShift1"
        RbShift1.Size = New Size(31, 19)
        RbShift1.TabIndex = 14
        RbShift1.TabStop = True
        RbShift1.Text = "1"
        RbShift1.UseVisualStyleBackColor = True
        ' 
        ' TB_Shift3Start
        ' 
        TB_Shift3Start.Location = New Point(113, 239)
        TB_Shift3Start.Name = "TB_Shift3Start"
        TB_Shift3Start.Size = New Size(60, 23)
        TB_Shift3Start.TabIndex = 13
        ' 
        ' LblShift3Start
        ' 
        LblShift3Start.Location = New Point(6, 242)
        LblShift3Start.Name = "LblShift3Start"
        LblShift3Start.Size = New Size(100, 15)
        LblShift3Start.TabIndex = 12
        LblShift3Start.Text = "Shift 3 Start:"
        LblShift3Start.TextAlign = ContentAlignment.TopRight
        ' 
        ' TB_Shift2Start
        ' 
        TB_Shift2Start.Location = New Point(113, 210)
        TB_Shift2Start.Name = "TB_Shift2Start"
        TB_Shift2Start.Size = New Size(60, 23)
        TB_Shift2Start.TabIndex = 11
        ' 
        ' LblShift2Start
        ' 
        LblShift2Start.Location = New Point(6, 213)
        LblShift2Start.Name = "LblShift2Start"
        LblShift2Start.Size = New Size(100, 15)
        LblShift2Start.TabIndex = 10
        LblShift2Start.Text = "Shift 2 Start:"
        LblShift2Start.TextAlign = ContentAlignment.TopRight
        ' 
        ' TB_Shift1Start
        ' 
        TB_Shift1Start.Location = New Point(113, 181)
        TB_Shift1Start.Name = "TB_Shift1Start"
        TB_Shift1Start.Size = New Size(60, 23)
        TB_Shift1Start.TabIndex = 9
        ' 
        ' LblShift1Start
        ' 
        LblShift1Start.Location = New Point(6, 184)
        LblShift1Start.Name = "LblShift1Start"
        LblShift1Start.Size = New Size(100, 15)
        LblShift1Start.TabIndex = 8
        LblShift1Start.Text = "Shift 1 Start:"
        LblShift1Start.TextAlign = ContentAlignment.TopRight
        ' 
        ' BtChangeTextFile
        ' 
        BtChangeTextFile.Location = New Point(494, 119)
        BtChangeTextFile.Name = "BtChangeTextFile"
        BtChangeTextFile.Size = New Size(75, 23)
        BtChangeTextFile.TabIndex = 7
        BtChangeTextFile.Text = "Select"
        BtChangeTextFile.UseVisualStyleBackColor = True
        ' 
        ' BtChangePath
        ' 
        BtChangePath.Location = New Point(494, 86)
        BtChangePath.Name = "BtChangePath"
        BtChangePath.Size = New Size(75, 23)
        BtChangePath.TabIndex = 6
        BtChangePath.Text = "Select"
        BtChangePath.UseVisualStyleBackColor = True
        ' 
        ' TB_TextFileName
        ' 
        TB_TextFileName.Location = New Point(113, 116)
        TB_TextFileName.Name = "TB_TextFileName"
        TB_TextFileName.Size = New Size(376, 23)
        TB_TextFileName.TabIndex = 5
        ' 
        ' LblTextFileName
        ' 
        LblTextFileName.Location = New Point(6, 119)
        LblTextFileName.Name = "LblTextFileName"
        LblTextFileName.Size = New Size(100, 15)
        LblTextFileName.TabIndex = 4
        LblTextFileName.Text = "Text File: "
        LblTextFileName.TextAlign = ContentAlignment.TopRight
        ' 
        ' BtCloseSett
        ' 
        BtCloseSett.BackgroundImage = My.Resources.Resources.close
        BtCloseSett.BackgroundImageLayout = ImageLayout.Stretch
        BtCloseSett.Location = New Point(545, 7)
        BtCloseSett.Name = "BtCloseSett"
        BtCloseSett.Size = New Size(40, 40)
        BtCloseSett.TabIndex = 3
        BtCloseSett.UseVisualStyleBackColor = True
        ' 
        ' TB_LogFilePath
        ' 
        TB_LogFilePath.Location = New Point(113, 86)
        TB_LogFilePath.Name = "TB_LogFilePath"
        TB_LogFilePath.Size = New Size(376, 23)
        TB_LogFilePath.TabIndex = 2
        ' 
        ' Lbl_LogFilePath
        ' 
        Lbl_LogFilePath.Location = New Point(6, 89)
        Lbl_LogFilePath.Name = "Lbl_LogFilePath"
        Lbl_LogFilePath.Size = New Size(100, 15)
        Lbl_LogFilePath.TabIndex = 1
        Lbl_LogFilePath.Text = "Path to LOG files: "
        Lbl_LogFilePath.TextAlign = ContentAlignment.TopRight
        ' 
        ' BtClearSettings
        ' 
        BtClearSettings.Location = New Point(494, 153)
        BtClearSettings.Name = "BtClearSettings"
        BtClearSettings.Size = New Size(75, 42)
        BtClearSettings.TabIndex = 0
        BtClearSettings.Text = "Clear settings"
        BtClearSettings.UseVisualStyleBackColor = True
        ' 
        ' CB_IncludeShifts
        ' 
        CB_IncludeShifts.AutoSize = True
        CB_IncludeShifts.Location = New Point(18, 237)
        CB_IncludeShifts.Name = "CB_IncludeShifts"
        CB_IncludeShifts.Size = New Size(97, 19)
        CB_IncludeShifts.TabIndex = 0
        CB_IncludeShifts.Text = "Include Shifts"
        CB_IncludeShifts.UseVisualStyleBackColor = True
        ' 
        ' BtSettings
        ' 
        BtSettings.BackColor = Color.Transparent
        BtSettings.BackgroundImage = My.Resources.Resources.settings
        BtSettings.BackgroundImageLayout = ImageLayout.Stretch
        BtSettings.Location = New Point(812, 3)
        BtSettings.Name = "BtSettings"
        BtSettings.Size = New Size(40, 40)
        BtSettings.TabIndex = 7
        BtSettings.UseVisualStyleBackColor = False
        ' 
        ' BtCopyConsumption
        ' 
        BtCopyConsumption.BackColor = Color.Transparent
        BtCopyConsumption.BackgroundImage = My.Resources.Resources.Copy2Clip
        BtCopyConsumption.BackgroundImageLayout = ImageLayout.Stretch
        BtCopyConsumption.Location = New Point(766, 3)
        BtCopyConsumption.Name = "BtCopyConsumption"
        BtCopyConsumption.Size = New Size(40, 40)
        BtCopyConsumption.TabIndex = 8
        BtCopyConsumption.UseVisualStyleBackColor = False
        ' 
        ' BtCreateExcel
        ' 
        BtCreateExcel.BackColor = Color.Transparent
        BtCreateExcel.BackgroundImage = My.Resources.Resources.BtExcel
        BtCreateExcel.BackgroundImageLayout = ImageLayout.Stretch
        BtCreateExcel.Location = New Point(720, 3)
        BtCreateExcel.Name = "BtCreateExcel"
        BtCreateExcel.Size = New Size(40, 40)
        BtCreateExcel.TabIndex = 9
        BtCreateExcel.UseVisualStyleBackColor = False
        ' 
        ' CbMachSelector
        ' 
        CbMachSelector.DropDownStyle = ComboBoxStyle.DropDownList
        CbMachSelector.FormattingEnabled = True
        CbMachSelector.Location = New Point(18, 262)
        CbMachSelector.Name = "CbMachSelector"
        CbMachSelector.Size = New Size(171, 23)
        CbMachSelector.TabIndex = 23
        ' 
        ' Main_Form
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(864, 461)
        Controls.Add(CbMachSelector)
        Controls.Add(BtCreateExcel)
        Controls.Add(BtCopyConsumption)
        Controls.Add(GB_Settings)
        Controls.Add(BtSettings)
        Controls.Add(CB_IncludeShifts)
        Controls.Add(GB_Usage)
        Controls.Add(Txt_Read)
        Controls.Add(Selection_To)
        Controls.Add(Selection_From)
        Controls.Add(Selection_Text)
        Controls.Add(MonthCalendar)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Main_Form"
        GB_Settings.ResumeLayout(False)
        GB_Settings.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents MonthCalendar As MonthCalendar
    Private WithEvents Selection_Text As Label
    Private WithEvents Selection_From As Label
    Private WithEvents Selection_To As Label
    Private WithEvents Txt_Read As Label
    Friend WithEvents GB_Usage As GroupBox
    Friend WithEvents CB_IncludeShifts As CheckBox
    Friend WithEvents BtClearSettings As Button
    Friend WithEvents GB_Settings As GroupBox
    Friend WithEvents TB_LogFilePath As TextBox
    Friend WithEvents Lbl_LogFilePath As Label
    Friend WithEvents BtSettings As Button
    Friend WithEvents BtCloseSett As Button
    Friend WithEvents TB_TextFileName As TextBox
    Friend WithEvents LblTextFileName As Label
    Friend WithEvents BtChangeTextFile As Button
    Friend WithEvents BtChangePath As Button
    Friend WithEvents TB_Shift3Start As TextBox
    Friend WithEvents LblShift3Start As Label
    Friend WithEvents TB_Shift2Start As TextBox
    Friend WithEvents LblShift2Start As Label
    Friend WithEvents TB_Shift1Start As TextBox
    Friend WithEvents LblShift1Start As Label
    Friend WithEvents RbShift3 As RadioButton
    Friend WithEvents RbShift2 As RadioButton
    Friend WithEvents LblNrOfShifts As Label
    Friend WithEvents RbShift1 As RadioButton
    Friend WithEvents BtCopyConsumption As Button
    Friend WithEvents BtCreateExcel As Button
    Friend WithEvents BtSaveSettings As Button
    Friend WithEvents TbMachNr As TextBox
    Friend WithEvents LblMachNr As Label
    Friend WithEvents TbMachName As TextBox
    Friend WithEvents LblMachName As Label
    Friend WithEvents CbMachSelector As ComboBox
    Friend WithEvents BtEditIngr As Button

End Class

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
        MonthCalendar = New MonthCalendar()
        Selection_Text = New Label()
        Selection_From = New Label()
        Selection_To = New Label()
        Txt_Read = New Label()
        GB_Usage = New GroupBox()
        SuspendLayout()
        ' 
        ' MonthCalendar
        ' 
        MonthCalendar.Location = New Point(18, 66)
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
        GB_Usage.Location = New Point(267, 9)
        GB_Usage.Name = "GB_Usage"
        GB_Usage.Size = New Size(585, 440)
        GB_Usage.TabIndex = 5
        GB_Usage.TabStop = False
        GB_Usage.Text = "Consumption"
        ' 
        ' Main_Form
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(864, 461)
        Controls.Add(GB_Usage)
        Controls.Add(Txt_Read)
        Controls.Add(Selection_To)
        Controls.Add(Selection_From)
        Controls.Add(Selection_Text)
        Controls.Add(MonthCalendar)
        Name = "Main_Form"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents MonthCalendar As MonthCalendar
    Private WithEvents Selection_Text As Label
    Private WithEvents Selection_From As Label
    Private WithEvents Selection_To As Label
    Private WithEvents Txt_Read As Label
    Friend WithEvents GB_Usage As GroupBox

End Class

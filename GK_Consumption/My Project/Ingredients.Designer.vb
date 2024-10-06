<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Ingredients
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        IngredientsView = New DataGridView()
        CType(IngredientsView, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' IngredientsView
        ' 
        IngredientsView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        IngredientsView.Location = New Point(12, 12)
        IngredientsView.Name = "IngredientsView"
        IngredientsView.RowTemplate.Height = 25
        IngredientsView.ScrollBars = ScrollBars.Vertical
        IngredientsView.Size = New Size(560, 537)
        IngredientsView.TabIndex = 0
        ' 
        ' Ingredients
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(584, 561)
        Controls.Add(IngredientsView)
        MaximizeBox = False
        MinimizeBox = False
        Name = "Ingredients"
        StartPosition = FormStartPosition.CenterParent
        Text = "Ingredients"
        TopMost = True
        CType(IngredientsView, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents IngredientsView As DataGridView
End Class

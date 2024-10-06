Public Class Ingredients

    ' This will store the current selected machine configuration
    Private selectedMachine As XML.MachineConfiguration = XML.MachineConfigurations(Main_Form.TbMachNr.Text)

    Private Sub Ingredients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BindIngredientsToDataGridView()
    End Sub


    Private Sub Ingredients_Close(sender As Object, e As EventArgs) Handles MyBase.Closing
        SaveIngredientsView()
    End Sub


    ' Bind the ingredients of the selected machine to the DataGridView
    Private Sub BindIngredientsToDataGridView()
        ' Prepare a DataTable to hold ingredients data
        Dim dt As New DataTable()
        dt.Columns.Add("Key", GetType(Integer))
        dt.Columns("Key").ReadOnly = True
        dt.Columns.Add("Name", GetType(String))
        dt.Columns.Add("Unit", GetType(String))
        dt.Columns.Add("ConversionFactor", GetType(Double))

        ' Fill DataTable with the ingredients
        For Each ingredient In SelectedMachine.Ingredients
            dt.Rows.Add(ingredient.Key, ingredient.Ingredient.Name, ingredient.Ingredient.Unit, ingredient.Ingredient.ConversionFactor)
        Next

        ' Bind DataTable to DataGridView and set sizes and allowances
        IngredientsView.DataSource = dt
        With IngredientsView
            .ColumnHeadersDefaultCellStyle.Font = New Font(.Font, FontStyle.Bold)
            .Columns("Key").Width = 50
            .Columns("Name").Width = 200
            .Columns("Unit").Width = 100
            .Columns("ConversionFactor").Width = 300
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
        End With
    End Sub




    Private Sub SaveIngredientsView()
        For i As Integer = 0 To IngredientsView.Rows.Count - 1
            ' Get the DataGridView row
            Dim row As DataGridViewRow = IngredientsView.Rows(i)

            ' Update the corresponding ingredient in the SelectedMachine.Ingredients
            Dim ingredient As XML.SerializableIngredient = selectedMachine.Ingredients(i)

            ' Update the properties based on the DataGridView row value
            With ingredient
                .Key = Convert.ToInt32(row.Cells("Key").Value)
                .Ingredient.Name = Convert.ToString(row.Cells("Name").Value)
                .Ingredient.Unit = Convert.ToString(row.Cells("Unit").Value)
                .Ingredient.ConversionFactor = Convert.ToDouble(row.Cells("ConversionFactor").Value)
            End With
        Next
    End Sub

End Class
Imports System.IO
Imports System.Xml.Serialization
Imports DocumentFormat.OpenXml.Packaging
Imports GK_Consumption.XML
Imports Irony

Public Class XML

    ' Define the file path for the machine configuration XML
    Public Shared configFileName As String '= "c:\gk\MachineConfig.xml"
    Shared Sub New()
        ' Conditionally check if we are in DEBUG mode
#If DEBUG Then
        ' Use a hardcoded path during development/debugging
        configFileName = "C:\gk\MachineConfig.xml"
#Else
            ' Use the application startup path for the release version
            configFileName = IO.Path.Combine(Application.StartupPath, "MachineConfig.xml")
#End If
    End Sub


    ' Container class for multiple machine configurations
    <XmlRoot("MachineConfigurationCollection")>
    Public Class MachineConfigurationCollection
        <XmlElement("MachineConfiguration")>
        Public Property MachineConfigurations As List(Of MachineConfiguration)

        ' Constructor
        Public Sub New()
            MachineConfigurations = New List(Of MachineConfiguration)()
        End Sub
    End Class


    ' A class that contains both settings and ingredients
    Public Class MachineConfiguration
        <XmlElement("MachineNumber")>
        Public Property MachineNumber As String

        <XmlElement("Settings")>
        Public Property Settings As Settings

        <XmlArray("Ingredients")>
        <XmlArrayItem("SerializableIngredient")>
        Public Property Ingredients As List(Of SerializableIngredient)

        ' Constructor
        Public Sub New()
        End Sub

        Public Sub New(machineNumber As String, settings As Settings, ingredients As List(Of SerializableIngredient))
            Me.MachineNumber = machineNumber
            Me.Settings = settings
            Me.Ingredients = ingredients
        End Sub
    End Class


    Public Class Settings
        <XmlElement("CustomerName")>
        Public Property CustomerName As String
        <XmlElement("LogDir")>
        Public Property LogDir As String
        <XmlElement("TextFileName")>
        Public Property TextFileName As String
        <XmlElement("NrOfShifts")>
        Public Property NrOfShifts As Integer
        <XmlArray("Shifts"), XmlArrayItem("Shift")>
        Public Property ShiftTimes As String()

        ' Property to convert ShiftTimes to TimeOnly array
        <XmlIgnore>
        Public Property Shifts As TimeOnly()
            Get
                'Return ShiftTimes.Select(Function(t) TimeOnly.Parse(t)).ToArray()
                If ShiftTimes Is Nothing Then
                    Return New TimeOnly() {New TimeOnly(6, 0, 0), New TimeOnly(14, 0, 0), New TimeOnly(22, 0, 0)} ' Default array
                End If
                Return ShiftTimes.Select(Function(t)
                                             Dim result As TimeOnly
                                             If TimeOnly.TryParse(t, result) Then
                                                 Return result
                                             Else
                                                 ' Return a default value (midnight) or handle as needed
                                                 Return New TimeOnly(0, 0)
                                             End If
                                         End Function).ToArray()
            End Get

            Set(value As TimeOnly())
                ' Always ensure ShiftTimes is initialized before setting values
                If value IsNot Nothing Then
                    ShiftTimes = value.Select(Function(t) t.ToString("HH:mm")).ToArray()
                Else
                    ' If value is Nothing, provide default values
                    ShiftTimes = New String() {"06:00", "14:00", "22:00"}
                End If
            End Set
        End Property

        Public Sub New()
            ShiftTimes = New String() {"06:00", "14:00", "22:00"}
        End Sub

        Public Sub New(customername As String, logdir As String, textfilename As String, nrofshifts As Integer, shifts As TimeOnly())
            Me.CustomerName = customername
            Me.LogDir = logdir
            Me.TextFileName = textfilename
            Me.NrOfShifts = nrofshifts
            ' Convert TimeOnly to string for serialization
            Me.ShiftTimes = shifts.Select(Function(t) t.ToString()).ToArray()
        End Sub
    End Class





    <XmlRoot("ArrayOfSerializableIngredient")>
    Public Class SerializableIngredient
        <XmlElement("Key")>
        Public Property Key As Integer

        <XmlElement("Ingredient")>
        Public Property Ingredient As Ingredient

        Public Sub New()
        End Sub

        Public Sub New(key As Integer, ingredient As Ingredient)
            Me.Key = key
            Me.Ingredient = ingredient
        End Sub
    End Class

    <Serializable>
    Public Class Ingredient
        <XmlElement("Name")>
        Public Property Name As String

        <XmlElement("Unit")>
        Public Property Unit As String

        <XmlElement("ConversionFactor")>
        Public Property ConversionFactor As Double

        Public Sub New()
        End Sub

        Public Sub New(name As String, unit As String, conversionFactor As Double)
            Me.Name = name
            Me.Unit = unit
            Me.ConversionFactor = conversionFactor
        End Sub
    End Class



    Public Shared MachineConfigurations As New Dictionary(Of String, MachineConfiguration)

    Public Shared Sub LoadMachineConfigurationsFromFile()

        ' Create an XML serializer for MachineConfigurationCollection
        Dim serializer As New XmlSerializer(GetType(MachineConfigurationCollection))

        ' Check if the file exists
        If File.Exists(configFileName) Then
            ' Deserialize the existing configurations
            Using reader As New StreamReader(configFileName)
                Dim deserializedCollection As MachineConfigurationCollection = CType(serializer.Deserialize(reader), MachineConfigurationCollection)

                ' Iterate through each machine configuration
                For Each newConfig As MachineConfiguration In deserializedCollection.MachineConfigurations
                    ' Check if the machine number already exists in the current collection
                    If Not MachineConfigurations.ContainsKey(newConfig.MachineNumber) Then
                        ' If it doesn't exist, add the new machine configuration
                        MachineConfigurations.Add(newConfig.MachineNumber, newConfig)
                    Else
                        ' If it exists, update the existing configuration
                        MachineConfigurations(newConfig.MachineNumber) = newConfig
                    End If
                Next
            End Using
        Else
            ' If the file does not exist, go to settings
            Main_Form.OpenSettings()
        End If
    End Sub


    Public Shared Sub SaveMachineConfigurationsToFile()

        ' Create an XML serializer
        Dim serializer As New XmlSerializer(GetType(MachineConfigurationCollection))

        ' Convert the MachineConfigurationCollection to a list
        Dim machineConfigList As New MachineConfigurationCollection()
        For Each config As MachineConfiguration In MachineConfigurations.Values
            machineConfigList.MachineConfigurations.Add(config)
        Next

        ' Save the list to the file
        Using writer As New StreamWriter(configFileName)
            serializer.Serialize(writer, machineConfigList)
        End Using
    End Sub

End Class

namespace Notes.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
	//When a note is created its saved to the device text file
	//string _fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
	public NotePage()
	{
		InitializeComponent();

		//Read the file from the device and store its contents in the texteditor
		string appDataPath = FileSystem.AppDataDirectory;
		string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

		LoadNote(Path.Combine(appDataPath, randomFileName));
    }

    //code to handle the Clicked events
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
		//Save the File.
		if (BindingContext is Models.Note note)
			File.WriteAllText(note.Filename, TextEditor.Text);
		
		await Shell.Current.GoToAsync("..");
    }

	private async void DeleteButton_Clicked(object sender, EventArgs e)
	{
		if (BindingContext is Models.Note note)
		{
            //Delete the file
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }

		await Shell.Current.GoToAsync("..");
	}

	private void LoadNote(string fileName)
	{
		Models.Note noteModel = new Models.Note();
		noteModel.Filename = fileName;

		if (File.Exists(fileName))
		{
			noteModel.Date = File.GetCreationTime(fileName);
			noteModel.Text = File.ReadAllText(fileName);
        }

		BindingContext = noteModel;
    }

    public string ItemId
    {
        set { LoadNote(value); }
    }
}
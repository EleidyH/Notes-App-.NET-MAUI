using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models;
//will handle loading, saving, and deleting notes.
internal class Note
{
    public string Filename { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }

    //A constructor which sets the default values for the properties, including a random file name.
    public Note()
    {
        Filename = $"{Path.GetRandomFileName()}.notes.txt";
        Date = DateTime.Now;
        Text = "";
    }

    //These methods are instance-based and handle saving or deleting the current note to or from the device.
    public void Save() =>
            File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename), Text);

    public void Delete() =>
            File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename));

    
    //These will load a note by file name.

    /*This code takes the file name as a parameter, builds the path to where the notes are stored on the device,
    and attempts to load the file if it exists */
    public static Note Load(string filename)
    {
        filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

        if (!File.Exists(filename))
            throw new FileNotFoundException("Unable to find file on local storage.", filename);

        return
            new()
            {
                Filename = Path.GetFileName(filename),
                Text = File.ReadAllText(filename),
                Date = File.GetLastWriteTime(filename)
            };
    }

    //The second way to load notes is to to enumerate all notes on the device and load them into a collection.

    /*This code returns an enumerable collection of "Note" model types by retrieving the files on the device that match the notes file pattern: *.notes.txt.
    Each file name is passed to the "Load" method, loading an individual note.
    Finally the collecion of notes is ordered by the date of each note and returned to the caller.*/
    public static IEnumerable<Note> LoadAll()
    {
        //Get the folder where the notes are stored.
        string appDataPath = FileSystem.AppDataDirectory;

        //Use Linq extensions to load the *.notes.txt files.
        return Directory

            //Select the file names from the directory.
            .EnumerateFiles(appDataPath, "*.notes.txt")

            //Each file name is used to load a note
            .Select(filename => Note.Load(Path.GetFileName(filename)))

            //With the final collection of notes, order them by date.
            .OrderByDescending(note => note.Date);
    }
}

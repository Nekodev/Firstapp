using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Tasky.Core;
using TaskyAndroid;

namespace TaskyAndroid.Screens {
	/// <summary>
	/// View/edit a Task
	/// </summary>
	[Activity (Label = "TaskDetailsScreen")]			
	public class TaskDetailsScreen : Activity {
		Task task = new Task();
		Button cancelDeleteButton;
		EditText notesTextEdit;
        EditText categoryTextEdit;
        EditText nameTextEdit;
		Button saveButton;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			int taskID = Intent.GetIntExtra("TaskID", 0);
			if(taskID > 0) {
				task = TaskManager.GetTask(taskID);
			}
			
			// set our layout to be the home screen
			SetContentView(Resource.Layout.TaskDetails);
			nameTextEdit = FindViewById<EditText>(Resource.Id.NameText);
            categoryTextEdit = FindViewById<EditText>(Resource.Id.CategoryText);
            notesTextEdit = FindViewById<EditText>(Resource.Id.NotesText);
			saveButton = FindViewById<Button>(Resource.Id.SaveButton);
			
			// find all our controls
			cancelDeleteButton = FindViewById<Button>(Resource.Id.CancelDeleteButton);
			
			// set the cancel delete based on whether or not it's an existing task
			cancelDeleteButton.Text = (task.ID == 0 ? "Cancel" : "Delete");
			
			nameTextEdit.Text = task.Name;
            categoryTextEdit.Text = task.Category;
            notesTextEdit.Text = task.Notes;

			// button clicks 
			cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };
			saveButton.Click += (sender, e) => { Save(); };
		}

		void Save()
		{
			task.Name = nameTextEdit.Text;
            task.Category = categoryTextEdit.Text;
            task.Notes = notesTextEdit.Text;
			TaskManager.SaveTask(task);
			Finish();
		}
		
		void CancelDelete()
		{
			if (task.ID != 0) {
				TaskManager.DeleteTask(task.ID);
			}
			Finish();
		}
	}
}
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ThreeTargets.Model;

namespace ThreeTargets.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class ManifestDetailViewModel : ViewModelBase
    {
        public ICommand DeleteCommand { get; set; }

        public Guid ID { get; set; }

        private string title;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                RaisePropertyChanged("Title");
            }
        }

        public ObservableCollection<ContentModel> Contents { get; set; }

        /// <summary>
        /// Initializes a new instance of the ManifestDetailViewModel class.
        /// </summary>
        public ManifestDetailViewModel()
        {
            DeleteCommand = new RelayCommand<string>((s) =>
            {
                Debug.WriteLine(s);
                var remove = (from c in Contents where c.Description.Equals(s) select c).FirstOrDefault();
                if (remove != null)
                {
                    Contents.Remove(remove);
                }
            });
            Update();
            Messenger.Default.Register<string>(this, Contract.UpdateDetail,
                s =>
                {
                    Update();
                });
            Messenger.Default.Register<string>(this, Contract.AddDesc,
                s =>
                {
                    Contents.Add(new ContentModel { Description = "" });
                });

            Messenger.Default.Register<string>(this, Contract.SaveManifest,
                s =>
                {
                    var manifest = new ManifestModel() { ID = this.ID, Title = this.title, Contents = this.Contents };
                    Messenger.Default.Send(manifest, Contract.UpdateMain);
                    IOManager.GetManager().UpdateManifest(manifest);
                });

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real": Connect to service, etc...
            ////}
        }

        private void Update()
        {
            var manifest = (App.Current as App).Manifests.Where(m => m.ID.Equals((App.Current as App).SelectedManifestID)).First();
            Title = manifest.Title;
            ID = manifest.ID;
            Contents = new ObservableCollection<ContentModel>();
            foreach (var c in manifest.Contents)
            {
                Contents.Add(c);
            }
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}
    }
}
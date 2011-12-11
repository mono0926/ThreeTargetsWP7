using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ThreeTargets.Model
{
    public class ManifestModel
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public IList<ContentModel> Contents { get; set; }

        public ManifestModel()
        {
            Contents = new List<ContentModel>();
        }
    }
}
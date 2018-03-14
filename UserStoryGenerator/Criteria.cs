using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStoryGenerator
{
    class Criteria
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }

        public string UserStoryId { get; set; }
        public virtual UserStory UserStory { get; set; }
    }
}

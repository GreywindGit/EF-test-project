using System.Collections.Generic;

namespace UserStoryGenerator
{
    class UserStory
    {
        public string Id { get; set; }
        public string Actor { get; set; }
        public string Goal { get; set; }
        public string Value { get; set; }

        public virtual ICollection<Criteria> Criterias { get; set; }
    }
}

using System.Threading.Tasks;

namespace RingCentral.Paths.Restapi.Glip.Teams.Archive
{
    public partial class Index
    {
        public RestClient rc;
        public Restapi.Glip.Teams.Index parent;

        public Index(Restapi.Glip.Teams.Index parent)
        {
            this.parent = parent;
            this.rc = parent.rc;
        }

        public string Path()
        {
            return $"{parent.Path()}/archive";
        }

        /// <summary>
        /// Operation: Archive Team
        /// Http Post /restapi/v1.0/glip/teams/{chatId}/archive
        /// </summary>
        public async Task<string> Post()
        {
            return await rc.Post<string>(this.Path());
        }
    }
}

namespace RingCentral.Paths.Restapi.Glip.Teams
{
    public partial class Index
    {
        public Restapi.Glip.Teams.Archive.Index Archive()
        {
            return new Restapi.Glip.Teams.Archive.Index(this);
        }
    }
}
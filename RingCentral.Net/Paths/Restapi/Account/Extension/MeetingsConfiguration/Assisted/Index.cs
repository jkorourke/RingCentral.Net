using System.Threading.Tasks;

namespace RingCentral.Paths.Restapi.Account.Extension.MeetingsConfiguration.Assisted
{
    public partial class Index
    {
        public RestClient rc;
        public Restapi.Account.Extension.MeetingsConfiguration.Index parent;

        public Index(Restapi.Account.Extension.MeetingsConfiguration.Index parent)
        {
            this.parent = parent;
            this.rc = parent.rc;
        }

        public string Path()
        {
            return $"{parent.Path()}/assisted";
        }

        /// <summary>
        /// Operation: Get Assisted Users
        /// Http Get /restapi/v1.0/account/{accountId}/extension/{extensionId}/meetings-configuration/assisted
        /// </summary>
        public async Task<RingCentral.AssistedUsersResource> Get()
        {
            return await rc.Get<RingCentral.AssistedUsersResource>(this.Path());
        }
    }
}

namespace RingCentral.Paths.Restapi.Account.Extension.MeetingsConfiguration
{
    public partial class Index
    {
        public Restapi.Account.Extension.MeetingsConfiguration.Assisted.Index Assisted()
        {
            return new Restapi.Account.Extension.MeetingsConfiguration.Assisted.Index(this);
        }
    }
}
using Newtonsoft.Json;

namespace RingCentral.Net
{
    public class ProfileImageResource : Serializable
    {
        public string uri;
        public string etag;
        public string contentType;
        public string lastModified;
        public ScaledProfileImageResource[] scales;
    }
}
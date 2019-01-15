using Newtonsoft.Json;

namespace RingCentral.Net
{
    public class MeetingRequestResource : Serializable
    {
        public string topic;
        public string meetingType;
        public MeetingScheduleResource schedule;
        public string password;
        public HostInfoRequest host;
        public bool? allowJoinBeforeHost;
        public bool? startHostVideo;
        public bool? startParticipantsVideo;
        public string[] audioOptions;
    }
}
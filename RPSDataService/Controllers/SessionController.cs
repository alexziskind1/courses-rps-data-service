using System.Collections.Generic;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using RPSDataService.DataObjects;

namespace RPSDataService.Controllers
{
    [MobileAppController]
    public class SessionController : ApiController
    {
        // GET api/Session
        public List<Session> Get()
        {
            var speakers = FakeDataService.GenerateSpeakers();
            var roomInfos = FakeDataService.GenerateRoomInfos();
            var sessions = FakeDataService.GenerateSessions(speakers, roomInfos);

            return sessions;
        }
    }
}

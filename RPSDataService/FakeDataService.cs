using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using RPSDataService.DataObjects;

namespace RPSDataService
{
    public static class FakeDataService
    {
        public const int NUM_SPEAKERS = 40;
        public const int NUM_ROOMINFOS = 10;
        public const int NUM_SESSIONS = 40;

        public static List<ConfDay> ConfDays
        {
            get
            {
                var list = new List<ConfDay> {
                    new ConfDay("Workshops", new DateTime(2016, 10, 3, 8, 0, 0), new DateTime(2016, 10, 3, 17, 0, 0) ),
                    new ConfDay("Conf Day 1", new DateTime(2016, 10, 4, 8, 0, 0), new DateTime(2016, 10, 4, 17, 0, 0) ),
                    new ConfDay("Conf Day 2", new DateTime(2016, 10, 5, 8, 0, 0), new DateTime(2016, 10, 5, 17, 0, 0) )
                };

                return list;
            }
        }

        public static List<Speaker> GenerateSpeakers()
        {
            List<string> allPhotos = Base64Avatars.MenPhotos.Union(Base64Avatars.WomenPhotos).ToList();

            var speakerList = new List<Speaker>();
            for (var i = 1; i <= NUM_SPEAKERS; i++)
            {
                var fullName = Faker.Name.FullName(Faker.NameFormats.Standard);

                Speaker s = new Speaker
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = fullName,
                    Company = Faker.Company.Name(),
                    Title = Faker.Company.CatchPhrase(),
                    TwitterName = "@" + fullName.Replace(" ", ""),
                    Picture = "data:image/png;base64," + allPhotos[Faker.RandomNumber.Next(0, allPhotos.Count - 1)]
                };
                speakerList.Add(s);
            }
            return speakerList;
        }

        public static List<RoomInfo> GenerateRoomInfos()
        {
            var roomInfos = new List<RoomInfo>();
            for (var i = 1; i <= NUM_ROOMINFOS; i++)
            {
                var r = new RoomInfo
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Faker.Address.StreetName() + " " + Faker.RandomNumber.Next(1, 10),
                    Url = Faker.Internet.DomainName(),
                    RoomId = Guid.NewGuid().ToString(),
                    Theme = Faker.Lorem.GetFirstWord()
                };
                roomInfos.Add(r);
            }
            return roomInfos;
        }

        public static List<Session> GenerateSessions(List<Speaker> speakers, List<RoomInfo> roomInfos)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            int idSeed = 1000;

            var sessions = new List<Session>();

            foreach (var confDay in ConfDays)
            {
                foreach (var confTimeSlot in confDay.TimeSlots)
                {
                    if (confTimeSlot.IsBreak)
                    {
                        var s = new Session
                        {
                            Id = (idSeed++).ToString(),
                            CalendarEventId = Guid.NewGuid().ToString(),
                            Room = string.Empty,
                            IsBreak = true,
                            Title = confTimeSlot.Title,
                            Start = confTimeSlot.Start,
                            End = confTimeSlot.End
                        };

                        sessions.Add(s);
                    }
                    else
                    {
                        var subSpeakers = PickSomeInRandomOrder<Speaker>(speakers, Faker.RandomNumber.Next(1, 3)).ToList();
                        var roomInfo = roomInfos[Faker.RandomNumber.Next(0, roomInfos.Count)];

                        var s = new Session
                        {
                            Id = (idSeed++).ToString(),
                            CalendarEventId = Guid.NewGuid().ToString(),
                            Description = Faker.Lorem.Paragraph(),
                            DescriptionShort = Faker.Lorem.Sentence(),
                            RoomInfo = roomInfo,
                            Room = roomInfo.Name,
                            IsBreak = Convert.ToBoolean(Faker.RandomNumber.Next(0, 1)),
                            Speakers = subSpeakers,
                            Title = textInfo.ToTitleCase(Faker.Company.BS()),
                            Start = confTimeSlot.Start,
                            End = confTimeSlot.End
                        };

                        sessions.Add(s);
                    }
                }
            }

            return sessions;
        }




        private static IEnumerable<SomeType> PickSomeInRandomOrder<SomeType>(IEnumerable<SomeType> someTypes, int maxCount)
        {
            Random random = new Random();

            Dictionary<double, SomeType> randomSortTable = new Dictionary<double, SomeType>();

            foreach (SomeType someType in someTypes)
                randomSortTable[Faker.RandomNumber.Next(1, 1000000)] = someType;

            return randomSortTable.OrderBy(KVP => KVP.Key).Take(maxCount).Select(KVP => KVP.Value);
        }


    }
}
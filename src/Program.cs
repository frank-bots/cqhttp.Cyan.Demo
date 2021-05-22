using System;
using System.Collections.Generic;
using System.Threading;
using cqhttp.Cyan.Clients;
using cqhttp.Cyan.Events.CQEvents;
using cqhttp.Cyan.Events.CQResponses;
using cqhttp.Cyan.Messages;
using cqhttp.Cyan.Utils;

namespace CyanBot {
    class Program {
        public static CQApiClient client;
        public static Dictionary<string, string> Globals =
            new Dictionary<string, string> ();
        static void Main (string[] args) {
            foreach (System.Collections.DictionaryEntry obj in Environment.GetEnvironmentVariables ())
                Globals.Add ((string) obj.Key, (string) obj.Value);
            client = new CQWebsocketClient (
                access_url: Globals["access_url"],
                event_url: Globals["event_url"],
                access_token: Globals["access_token"],
                use_group_table: true
            );

            Logger.GetLogger ("cqhttp.Cyan").log_level = cqhttp.Cyan.Enums.Verbosity.INFO;

            client.OnEventAsync += async (cli, e) => {
                switch (e) {
                case GroupMessageEvent gme:
                    break;
                case PrivateMessageEvent pme:
                    await cli.SendMessageAsync (pme.GetEndpoint (), new Message ("hello world!"));
                    break;
                }
                return new EmptyResponse ();
            };
            Thread.Sleep (Timeout.Infinite);
        }
    }
}

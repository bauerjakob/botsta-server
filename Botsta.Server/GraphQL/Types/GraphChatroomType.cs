﻿using System;
using System.Linq;
using Botsta.DataStorage.Entities;
using Botsta.Core.Services;
using GraphQL.Types;

namespace Botsta.Server.GraphQL.Types
{
    public class GraphChatroomType : ObjectGraphType<Chatroom>
    {
        public GraphChatroomType(ISessionController session)
        {
            Field(c => c.Id);
            Field("type", c => c.Type.ToString());
            Field<StringGraphType>("name", resolve: c => {
                var user = session.GetUser();

                var chatroom = c.Source;
                return chatroom.Name ?? string.Join(", ", c.Source.ChatPracticants.Where(p => p.Id != user.Id).Select(c => c.Name).OrderBy(n => n));
            });
            Field<GraphMessageType>("latestMessage", resolve: c => {
                    var sessionId = session.GetSessionId();
                    return c.Source.Messages?.OrderBy(m => m.SendTime).LastOrDefault(m => m.ReceiverSessionId == sessionId);
                });

            Field<ListGraphType<GraphMessageType>>(
                "messages",
                resolve: c => {
                    var sessionId = session.GetSessionId();

                    var chatroom = c.Source;
                    return chatroom.Messages.Where(m => m.ReceiverSessionId == sessionId).OrderByDescending(m => m.SendTime);
                }
                );
        }
    }
}

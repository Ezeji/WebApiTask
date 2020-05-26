using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTask.Data;
using ApiTask.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Repository
{
    public class Event : IEvent
    {
        private ApiTaskDBContext _context;

        public Event(ApiTaskDBContext context)
        {
            _context = context;
        }

        public async Task CreateEvent(UserEvents userEvents)
        {
            await _context.UserEvent.AddAsync(userEvents);
            await _context.SaveChangesAsync();
        }

        public async Task<string> ValidateEventId(UserEvents userEvents)
        {
            var eventcount = await _context.UserEvent
                .Where(eventId => eventId.EventId == userEvents.EventId)
                .CountAsync();

            if(eventcount > 0)
            {
                return "EventId already exists";
            }
            else
            {
                return "Event Created";
            }
        }
    }
}

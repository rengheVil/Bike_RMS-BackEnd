using BikeRentalMS.Database;
using BikeRentalMS.Enums;
using BikeRentalMS.Models;

namespace BikeRentalMS.Repositories
{
    public class SendMailRepository(AppDbContext _Context)
    {
        public async Task<EmailTemplate> GetTemplate(EmailTypes emailTypes)
        {
            var template = _Context.EmailTemplates.Where(x => x.emailTypes == emailTypes).FirstOrDefault();
            return template;
        }

    }
}

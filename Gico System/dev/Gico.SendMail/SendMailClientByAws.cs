using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Gico.Config;

namespace Gico.SendMail
{
    public class SendMailClientByAws : ISendMailClient
    {
        public async Task<bool> Send(string fromMail, string toMail, string title, string content, Ref<string> response)
        {
            try
            {
                var client = new AmazonSimpleEmailServiceClient(ConfigSettingEnum.AwsAccessKeyId.GetConfig(), ConfigSettingEnum.AwsSecretAccessKey.GetConfig(), RegionEndpoint.USWest2);
                var sendRequest = new SendEmailRequest
                {
                    Source = fromMail,
                    Destination = new Destination
                    {
                        ToAddresses = new List<string> { toMail }
                    },
                    Message = new Message
                    {
                        Subject = new Content(title),
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = content
                            },
                            //Text = new Content
                            //{
                            //    Charset = "UTF-8",
                            //    Data = textBody
                            //}
                        }
                    },
                    // If you are not using a configuration set, comment
                    // or remove the following line 
                    //ConfigurationSetName = configSet
                };
                try
                {
                    Console.WriteLine("Sending email using Amazon SES...");
                    var result = await client.SendEmailAsync(sendRequest);
                    response.Value = Common.Serialize.JsonSerializeObject(result);
                    Console.WriteLine("The email was sent successfully.");
                    return result.HttpStatusCode == HttpStatusCode.OK;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The email was not sent.");
                    Console.WriteLine("Error message: " + ex.Message);
                    throw ex;
                }
            }
            catch (Exception e)
            {
                e.Data["Param"] = new { fromMail, toMail, title, content };
                throw e;
            }
        }
    }
}
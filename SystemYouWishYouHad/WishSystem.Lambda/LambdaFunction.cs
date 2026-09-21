using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;

namespace WishSystem.Lambda;

public class LambdaFunction
{
    public void Handler(SQSEvent e, ILambdaContext context) 
    {

        foreach (var record in e.Records)
        {
            Console.WriteLine(record.Body);
        }
    }
}
namespace Aliyun.OpenServices.Common.Transform
{
    internal interface IDeserializer<TInput, TOutput>
    {
        TOutput Deserialize(TInput input);
    }
}


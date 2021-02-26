namespace BookingSample.Domain
{
    public interface IAddValidator<in T>
    {
        IValidationResult Validate(T obj);
    }
}
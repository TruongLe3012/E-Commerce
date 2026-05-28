using FluentValidation;
using Shop.Application.DTOs.Product;

namespace Shop.Application.Validators.Product
{
    public class CreateProductValidator
        : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Product name is required")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage(
                    "Price must be greater than 0");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.CategoryId)
                .GreaterThan(0);
        }
    }
}
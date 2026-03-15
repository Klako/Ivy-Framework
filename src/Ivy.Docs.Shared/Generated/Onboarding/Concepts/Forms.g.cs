using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:8, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/08_Forms.md", searchHints: ["input", "validation", "submission", "fields", "form", "builder", "useform", "form-builder", "form-submission", "form-handling"])]
public class FormsApp(bool onlyBody = false) : ViewBase
{
    public FormsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("forms", "Forms", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("automatic-field-generation", "Automatic Field Generation", 3), new ArticleHeading("field-configuration", "Field Configuration", 2), new ArticleHeading("custom-labels-and-descriptions", "Custom Labels and Descriptions", 3), new ArticleHeading("custom-input-builders", "Custom Input Builders", 3), new ArticleHeading("required-fields", "Required Fields", 3), new ArticleHeading("layout-control", "Layout Control", 2), new ArticleHeading("field-placement", "Field Placement", 3), new ArticleHeading("grouped-fields", "Grouped Fields", 3), new ArticleHeading("field-ordering-and-removal", "Field Ordering and Removal", 3), new ArticleHeading("validation", "Validation", 2), new ArticleHeading("custom-submit-text", "Custom Submit Text", 3), new ArticleHeading("form-validation", "Form Validation", 3), new ArticleHeading("display-attributes", "Display Attributes", 3), new ArticleHeading("programmatic-validation", "Programmatic Validation", 3), new ArticleHeading("validation-examples", "Validation Examples", 3), new ArticleHeading("collections-and-arrays", "Collections and Arrays", 4), new ArticleHeading("combining-multiple-validators", "Combining Multiple Validators", 4), new ArticleHeading("form-submission", "Form Submission", 2), new ArticleHeading("basic-form-submission", "Basic Form Submission", 3), new ArticleHeading("form-submission-with-state-updates", "Form Submission with State Updates", 3), new ArticleHeading("form-re-rendering", "Form Re-rendering", 3), new ArticleHeading("form-submit-strategies", "Form submit strategies", 3), new ArticleHeading("advanced-features", "Advanced Features", 2), new ArticleHeading("conditional-fields", "Conditional Fields", 3), new ArticleHeading("dynamic-field-configuration", "Dynamic Field Configuration", 3), new ArticleHeading("forms-in-ui-components", "Forms in UI Components", 2), new ArticleHeading("sheet-forms", "Sheet Forms", 3), new ArticleHeading("dialog-forms", "Dialog Forms", 3), new ArticleHeading("migration-from-manual-forms", "Migration from Manual Forms", 2), new ArticleHeading("useform", "UseForm", 2), new ArticleHeading("basic-usage", "Basic Usage", 3), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Forms").OnLinkClick(onLinkClick)
            | Lead("Build robust forms with built-in [state management](app://hooks/core/use-state), validation, and submission handling for collecting and processing user input.")
            | new Callout("Do not manually create form layouts. Always use `.ToForm()` on your [state](app://hooks/core/use-state) objects for type safety, automatic state management, and built-in validation.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Basic Usage
                
                The simplest way to create a form is to call `.ToForm()` on a [state](app://hooks/core/use-state) object. The FormBuilder automatically scaffolds appropriate input [fields](app://widgets/inputs/field) based on your model's property types, providing automatic state management and validation.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicFormExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicFormExample : ViewBase
                    {
                        public record UserModel(string Name, string Email, bool IsActive, int Age);
                    
                        public override object? Build()
                        {
                            var user = UseState(() => new UserModel("", "", false, 25));
                    
                            return user.ToForm();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout(
                """"
                **Automatic Email Validation**: Fields ending with "Email" (like `UserEmail`, `ContactEmail`) automatically get email validation, even without the `[EmailAddress]` attribute.
                """", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Automatic Field Generation
                
                The FormBuilder automatically maps C# types to appropriate [input widgets](app://onboarding/concepts/widgets):
                
                | C# Type | Generated Input | Notes |
                |---------|----------------|-------|
                | `string` | [TextInput](app://widgets/inputs/text-input) | Can be customized to email, password, textarea, etc. |
                | `bool`, `bool?` | [BoolInput](app://widgets/inputs/bool-input) | Checkbox or toggle |
                | `int`, `decimal`, `double` | [NumberInput](app://widgets/inputs/number-input) | Number input with validation |
                | `DateTime`, `DateOnly` | [DateTimeInput](app://widgets/inputs/date-time-input) | Date/time picker |
                | `Enum` | [SelectInput](app://widgets/inputs/select-input) | Dropdown with enum values |
                | `List<Enum>` | [SelectInput](app://widgets/inputs/select-input) with multiple selection | Multi-select dropdown |
                | `string` with `[AllowedValues]` | [SelectInput](app://widgets/inputs/select-input) | Auto-generates dropdown from allowed values |
                | `string[]` with `[AllowedValues]` | [SelectInput](app://widgets/inputs/select-input) with multiple selection | Auto-generates multi-select from allowed values |
                | Properties ending in "Id" | [ReadOnlyInput](app://widgets/inputs/read-only-input) | Typically for system-generated IDs |
                | Properties ending in "Email" | [TextInput](app://widgets/inputs/text-input) with email validation | Email-specific input |
                | Properties ending in "Password" | PasswordInput | Hidden text input |
                
                ## Field Configuration
                
                ### Custom Labels and Descriptions
                
                Use `.Label()`, `.Description()`, and `.Help()` to customize [field](app://widgets/inputs/field) appearance and provide help text.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ConfiguredFormExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ConfiguredFormExample : ViewBase
                    {
                        public record ContactModel(
                            string Name,
                            string Email,
                            string Phone,
                            string Message,
                            bool Subscribe,
                            Gender Gender = Gender.Other
                        );
                    
                        public enum Gender { Male, Female, Other }
                    
                        public override object? Build()
                        {
                            var contact = UseState(() => new ContactModel("", "", "", "", false));
                    
                            return contact.ToForm()
                                .Label(m => m.Name, "Full Name")
                                .Description(m => m.Name, "Enter your full name as it appears on official documents")
                                .Label(m => m.Email, "Email Address")
                                .Help(m => m.Email, "We'll use this email to send you updates and important notifications")
                                .Description(m => m.Email, "We'll use this to send you updates")
                                .Label(m => m.Phone, "Phone Number")
                                .Label(m => m.Message, "Your Message")
                                .Required(m => m.Name, m => m.Email);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Custom Input Builders
                
                Use `.Builder()` to specify custom input types for specific [fields](app://widgets/inputs/field).
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CustomInputsExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CustomInputsExample : ViewBase
                    {
                        public record ProductModel(
                            string Name,
                            string Description,
                            decimal Price,
                            string JsonConfig,
                            List<string> Tags,
                            DateTime ReleaseDate
                        );
                    
                        public override object? Build()
                        {
                            var product = UseState(() => new ProductModel("", "", 0.0m, "{}", new(), DateTime.Now));
                    
                            // Create sample tag options for the multi-select
                            var tagOptions = new[] { "Electronics", "Clothing", "Books", "Home", "Sports", "Food" }.ToOptions();
                    
                            return product.ToForm()
                                .Builder(m => m.Description, s => s.ToTextareaInput())
                                .Builder(m => m.JsonConfig, s => s.ToCodeInput().Language(Languages.Json))
                                .Builder(m => m.Tags, s => s.ToSelectInput(tagOptions))
                                .Builder(m => m.ReleaseDate, s => s.ToDateTimeInput())
                                .Label(m => m.Description, "Product Description")
                                .Label(m => m.JsonConfig, "Configuration (JSON)")
                                .Label(m => m.Tags, "Product Tags")
                                .Label(m => m.ReleaseDate, "Release Date");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Required Fields
                
                Mark [fields](app://widgets/inputs/field) as required using `.Required()` or rely on automatic detection from non-nullable types.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new RequiredFieldsExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class RequiredFieldsExample : ViewBase
                    {
                        public record OrderModel(
                            string CustomerName,
                            string? CustomerEmail,
                            string ShippingAddress,
                            int Quantity,
                            bool IsPriority
                        );
                    
                        public override object? Build()
                        {
                            var order = UseState(() => new OrderModel("", null, "", 1, false));
                    
                            return order.ToForm()
                                .Required(m => m.CustomerEmail)
                                .Required(m => m.IsPriority)
                                .Label(m => m.CustomerName, "Customer Name")
                                .Label(m => m.CustomerEmail, "Email Address")
                                .Label(m => m.ShippingAddress, "Shipping Address")
                                .Help(m => m.ShippingAddress, "Enter the complete shipping address including street, city, and postal code")
                                .Label(m => m.Quantity, "Quantity")
                                .Label(m => m.IsPriority, "Priority Order");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Layout Control
                
                ### Field Placement
                
                Control [field](app://widgets/inputs/field) placement using `.Place()` and `.PlaceHorizontal()` methods for custom [layouts](app://onboarding/concepts/layout).
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new LayoutControlExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class LayoutControlExample : ViewBase
                    {
                        public record AddressModel(
                            string Street,
                            string City,
                            string State,
                            string ZipCode,
                            string Country
                        );
                    
                        public override object? Build()
                        {
                            var address = UseState(() => new AddressModel("", "", "", "", ""));
                    
                            return address.ToForm()
                                .Place(m => m.Street)                    // Single field spans full width
                                .PlaceHorizontal(m => m.City, m => m.State)  // Two fields side-by-side, sharing row width
                                .PlaceHorizontal(m => m.ZipCode, m => m.Country) // Two fields side-by-side, sharing row width
                                .Label(m => m.Street, "Street Address")
                                .Label(m => m.City, "City")
                                .Label(m => m.State, "State/Province")
                                .Label(m => m.ZipCode, "ZIP/Postal Code")
                                .Label(m => m.Country, "Country");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Grouped Fields
                
                Organize related [fields](app://widgets/inputs/field) into logical groups using `.Group()`.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new GroupedFieldsExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class GroupedFieldsExample : ViewBase
                    {
                        public record EmployeeModel(
                            string FirstName,
                            string LastName,
                            string Email,
                            string Department,
                            decimal Salary,
                            DateTime HireDate,
                            string Street,
                            string City,
                            string State
                        );
                    
                        public override object? Build()
                        {
                            var employee = UseState(() => new EmployeeModel("", "", "", "", 0.0m, DateTime.Now, "", "", ""));
                    
                            return employee.ToForm()
                                .Group("Personal Information", m => m.FirstName, m => m.LastName, m => m.Email)
                                .Group("Employment", m => m.Department, m => m.Salary, m => m.HireDate)
                                .Group("Address", m => m.Street, m => m.City, m => m.State)
                                .Label(m => m.FirstName, "First Name")
                                .Label(m => m.LastName, "Last Name")
                                .Label(m => m.Email, "Email Address")
                                .Label(m => m.Department, "Department")
                                .Label(m => m.Salary, "Annual Salary")
                                .Label(m => m.HireDate, "Hire Date")
                                .Label(m => m.Street, "Street Address")
                                .Label(m => m.City, "City")
                                .Label(m => m.State, "State");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Field Ordering and Removal
                
                Control which [fields](app://widgets/inputs/field) are shown and in what order using `.Clear()`, `.Add()`, and `.Remove()`.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new FieldManagementExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class FieldManagementExample : ViewBase
                    {
                        public record ProductModel(
                            string Name,
                            string Description,
                            decimal Price,
                            string Category,
                            int Stock,
                            string SKU,
                            DateTime CreatedDate
                        );
                    
                        public override object? Build()
                        {
                            var product = UseState(() => new ProductModel("", "", 0.0m, "", 0, "", DateTime.Now));
                    
                            return product.ToForm()
                                .Clear()                                    // Hide all auto-generated fields
                                .Add(m => m.Name)                          // Show Name first
                                .Add(m => m.Description)                   // Show Description second
                                .Add(m => m.Price)                         // Show Price third
                                .Add(m => m.Category)                      // Show Category fourth
                                .Add(m => m.Stock)                         // Show Stock last
                                .Remove(m => m.SKU)                        // Hide SKU field
                                .Remove(m => m.CreatedDate)                // Hide CreatedDate field
                                .Label(m => m.Name, "Product Name")
                                .Label(m => m.Description, "Product Description")
                                .Label(m => m.Price, "Unit Price")
                                .Label(m => m.Category, "Product Category")
                                .Label(m => m.Stock, "Available Stock");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Validation
                
                ### Custom Submit Text
                
                Change the text of the submit button by passing it as a parameter of the `.ToForm()` method
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CustomSubmitTitleFormExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CustomSubmitTitleFormExample : ViewBase
                    {
                        public record UserModel(string Name, string Email, bool IsActive, int Age);
                    
                        public override object? Build()
                        {
                            var user = UseState(() => new UserModel("", "", false, 25));
                    
                            return user.ToForm("Create new user");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Form Validation
                
                Forms support automatic validation using standard .NET DataAnnotations, with the ability to add custom validation logic for specific business rules. Validation errors appear when you try to submit the form.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ValidationExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ValidationExample : ViewBase
                    {
                        public class UserModel
                        {
                            [Required(ErrorMessage = "Username is required")]
                            [Length(3, 50, ErrorMessage = "Username must be between 3 and 50 characters")]
                            public string Username { get; set; } = "";
                    
                            [Required]
                            [EmailAddress(ErrorMessage = "Please enter a valid email address")]
                            [MaxLength(100)]
                            public string Email { get; set; } = "";
                    
                            [Required]
                            [Length(8, 100, ErrorMessage = "Password must be between 8 and 100 characters")]
                            [DataType(DataType.Password)]
                            public string Password { get; set; } = "";
                    
                            [Range(13, 120, ErrorMessage = "Age must be between 13 and 120")]
                            public int Age { get; set; } = 18;
                    
                            [Phone(ErrorMessage = "Please enter a valid phone number")]
                            public string? PhoneNumber { get; set; }
                    
                            [Url(ErrorMessage = "Please enter a valid URL")]
                            public string? Website { get; set; }
                    
                            [AllowedValues("USA", "Canada", "UK", ErrorMessage = "Please select a valid country")]
                            public string Country { get; set; } = "USA";
                    
                            [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "ZIP code must be in format 12345 or 12345-6789")]
                            public string? ZipCode { get; set; }
                    
                            [CreditCard(ErrorMessage = "Please enter a valid credit card number")]
                            [Length(13, 19, ErrorMessage = "Credit card number must be between 13 and 19 digits")]
                            public string? CreditCardNumber { get; set; }
                    
                            public DateTime BirthDate { get; set; } = DateTime.Now;
                        }
                    
                        public override object? Build()
                        {
                            var user = UseState(() => new UserModel());
                            var client = UseService<IClientProvider>();
                    
                            UseEffect(() =>
                            {
                                // This only fires when the form is submitted successfully (passes validation)
                                if (!string.IsNullOrEmpty(user.Value.Username))
                                {
                                    client.Toast($"Account created for {user.Value.Username}!");
                                }
                            }, user);
                    
                            return user.ToForm("Create Account")
                                // Custom validation: birth date cannot be in the future
                                .Validate<DateTime>(m => m.BirthDate, birthDate =>
                                    (birthDate <= DateTime.Now, "Birth date cannot be in the future"))
                                // Custom validation: username cannot contain spaces
                                .Validate<string>(m => m.Username, username =>
                                    (!username.Contains(' '), "Username cannot contain spaces"));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("**Supported DataAnnotations**: Forms support all standard .NET DataAnnotations including `[Required]`, `[Length]`, `[MinLength]`, `[MaxLength]`, `[Range]`, `[EmailAddress]`, `[Phone]`, `[Url]`, `[CreditCard]`, `[RegularExpression]`, `[AllowedValues]`, and `[DataType]`. All attributes support custom error messages via the `ErrorMessage` parameter.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Display Attributes
                
                The `[Display]` attribute provides powerful control over how [fields](app://widgets/inputs/field) appear in your forms without requiring additional configuration code.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DisplayAttributeExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class DisplayAttributeExample : ViewBase
                    {
                        public class UserRegistrationModel
                        {
                            [Display(Name = "Full Name", Description = "Enter your complete legal name", Order = 1)]
                            [Required(ErrorMessage = "Full name is required")]
                            [Length(2, 100)]
                            public string Name { get; set; } = "";
                    
                            [Display(Name = "Email Address", Description = "We'll use this for account verification", Order = 2)]
                            [Required]
                            [EmailAddress]
                            public string Email { get; set; } = "";
                    
                            [Display(Name = "Phone Number", Description = "For account security purposes", Prompt = "+1-234-567-8900", Order = 3)]
                            [Phone]
                            public string? PhoneNumber { get; set; }
                    
                            [Display(GroupName = "Account Security", Name = "Password", Order = 4)]
                            [Required]
                            [Length(8, 100)]
                            [DataType(DataType.Password)]
                            public string Password { get; set; } = "";
                    
                            [Display(GroupName = "Account Security", Name = "Confirm Password", Order = 5)]
                            [Required]
                            [DataType(DataType.Password)]
                            public string ConfirmPassword { get; set; } = "";
                    
                            [Display(GroupName = "Preferences", Name = "Newsletter Subscription", Description = "Receive weekly updates", Order = 6)]
                            public bool SubscribeToNewsletter { get; set; } = false;
                    
                            [Display(GroupName = "Preferences", Name = "Preferred Theme", Order = 7)]
                            [AllowedValues("Light", "Dark", "Auto")]
                            public string Theme { get; set; } = "Auto";
                        }
                    
                        public override object? Build()
                        {
                            var user = UseState(() => new UserRegistrationModel());
                            var client = UseService<IClientProvider>();
                    
                            UseEffect(() =>
                            {
                                if (!string.IsNullOrEmpty(user.Value.Name))
                                {
                                    client.Toast($"Registration completed for {user.Value.Name}!");
                                }
                            }, user);
                    
                            return user.ToForm("Create Account")
                                .Builder(m => m.Password, s => s.ToPasswordInput())
                                .Builder(m => m.ConfirmPassword, s => s.ToPasswordInput())
                                .Validate<string>(m => m.ConfirmPassword, confirmPassword =>
                                    (confirmPassword == user.Value.Password, "Passwords must match"));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                The `[Display]` attribute supports these properties:
                
                - **Name**: Custom [field](app://widgets/inputs/field) label (overrides automatic label generation)
                - **Description**: Help text shown below the field
                - **Order**: Field ordering (lower numbers appear first)
                - **GroupName**: Groups related fields together
                - **Prompt**: Placeholder text for input [fields](app://widgets/inputs/field)
                
                ### Programmatic Validation
                
                In addition to DataAnnotations, you can add validation programmatically using FormBuilder methods:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ProgrammaticValidationExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ProgrammaticValidationExample : ViewBase
                    {
                        public record ProductModel(
                            string Name,
                            string? Description,
                            decimal Price,
                            int Stock
                        );
                    
                        public override object? Build()
                        {
                            var product = UseState(() => new ProductModel("", null, 0.0m, 0));
                    
                            return product.ToForm()
                                // Mark fields as required programmatically
                                .Required(m => m.Name, m => m.Price)
                                // Add custom validation logic
                                .Validate<string>(m => m.Name, name =>
                                    (name.Length >= 3, "Product name must be at least 3 characters"))
                                .Validate<decimal>(m => m.Price, price =>
                                    (price > 0, "Price must be greater than zero"))
                                .Validate<int>(m => m.Stock, stock =>
                                    (stock >= 0, "Stock cannot be negative"))
                                .Label(m => m.Name, "Product Name")
                                .Label(m => m.Description, "Description")
                                .Label(m => m.Price, "Price")
                                .Label(m => m.Stock, "Stock Quantity");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                **Programmatic Validation Methods:**
                
                - `.Required(params Expression<Func<TModel, object>>[] fields)` - Mark [fields](app://widgets/inputs/field) as required
                - `.Validate<T>(Expression<Func<TModel, object>> field, Func<T, (bool, string)> validator)` - Add custom validation with custom error message
                
                ### Validation Examples
                
                #### Collections and Arrays
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CollectionValidationExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CollectionValidationExample : ViewBase
                    {
                        public class SurveyModel
                        {
                            [Display(Name = "Interests", Description = "Select at least one interest")]
                            [Required(ErrorMessage = "Please select at least one interest")]
                            [MinLength(1, ErrorMessage = "You must select at least one interest")]
                            [AllowedValues("Technology", "Sports", "Music", "Art", "Travel")]
                            public string[] Interests { get; set; } = Array.Empty<string>();
                    
                            [Display(Name = "Tags")]
                            [Length(1, 5, ErrorMessage = "Select between 1 and 5 tags")]
                            public List<string> Tags { get; set; } = new();
                        }
                    
                        public override object? Build()
                        {
                            var survey = UseState(() => new SurveyModel());
                            var tagOptions = new[] { "New", "Popular", "Featured", "Sale", "Limited" }.ToOptions();
                    
                            return survey.ToForm()
                                .Builder(m => m.Tags, s => s.ToSelectInput(tagOptions).List());
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("#### Combining Multiple Validators").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new MultipleValidatorsExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class MultipleValidatorsExample : ViewBase
                    {
                        public class AccountModel
                        {
                            [Display(Name = "Username")]
                            [Required(ErrorMessage = "Username is required")]
                            [Length(3, 20, ErrorMessage = "Username must be between 3 and 20 characters")]
                            [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores")]
                            public string Username { get; set; } = "";
                    
                            [Display(Name = "Email")]
                            [Required]
                            [EmailAddress(ErrorMessage = "Invalid email format")]
                            [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
                            public string Email { get; set; } = "";
                    
                            [Display(Name = "Password")]
                            [Required]
                            [Length(8, 128, ErrorMessage = "Password must be between 8 and 128 characters")]
                            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)", ErrorMessage = "Password must contain uppercase, lowercase, and a number")]
                            [DataType(DataType.Password)]
                            public string Password { get; set; } = "";
                        }
                    
                        public override object? Build()
                        {
                            var account = UseState(() => new AccountModel());
                    
                            return account.ToForm("Create Account")
                                .Builder(m => m.Password, s => s.ToPasswordInput());
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Form Submission
                
                ### Basic Form Submission
                
                Forms automatically handle submission when the user presses Enter or clicks the built-in submit button. When a form is submitted successfully, it updates the model state, which triggers any [UseEffect](app://hooks/core/use-effect) watching that state.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new FormSubmissionExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class FormSubmissionExample : ViewBase
                    {
                        public record ContactModel(string Name, string Email, string Message);
                    
                        public override object? Build()
                        {
                            var contact = UseState(() => new ContactModel("", "", ""));
                            var client = UseService<IClientProvider>();
                    
                            UseEffect(() =>
                            {
                                if (!string.IsNullOrEmpty(contact.Value.Name))
                                {
                                    client.Toast($"Message from {contact.Value.Name} sent successfully!");
                                }
                            }, contact);
                    
                            return contact.ToForm()
                                .Required(m => m.Name, m => m.Email, m => m.Message);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Form Submission with State Updates
                
                React to form submission by watching the model state with [UseEffect](app://hooks/core/use-effect). The form automatically updates the state when submitted successfully, triggering [state changes](app://hooks/core/use-state) and UI updates.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new FormStatesExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class FormStatesExample : ViewBase
                    {
                        public record OrderModel(
                            string CustomerName,
                            string ProductName,
                            int Quantity,
                            decimal UnitPrice
                        );
                    
                        public override object? Build()
                        {
                            var order = UseState(() => new OrderModel("", "", 1, 0.0m));
                            var client = UseService<IClientProvider>();
                    
                            UseEffect(() =>
                            {
                                if (!string.IsNullOrEmpty(order.Value.CustomerName))
                                {
                                    var total = order.Value.Quantity * order.Value.UnitPrice;
                                    client.Toast($"Order created! Total: ${total:F2}");
                                }
                            }, order);
                    
                            return Layout.Vertical()
                                | order.ToForm()
                                    .Required(m => m.CustomerName, m => m.ProductName, m => m.Quantity, m => m.UnitPrice)
                                | Text.Block($"Total: ${order.Value.Quantity * order.Value.UnitPrice:F2}");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Form Re-rendering
                
                Demonstrates how to update form state and trigger re-renders:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SimpleFormWithResetExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SimpleFormWithResetExample : ViewBase
                    {
                        public record MyModel(string Name, string Email, int Age);
                    
                        public override object? Build()
                        {
                            // Create the state for your model
                            var model = UseState(() => new MyModel("", "", 0));
                    
                            // Create a form from the state
                            var form = model.ToForm()
                                .Label(m => m.Name, "Full Name")
                                .Label(m => m.Email, "Email Address")
                                .Label(m => m.Age, "Age");
                    
                            // To update the model and trigger re-render, you MUST use Set()
                            UseEffect(async () =>
                            {
                                // Example: Load data after 2 seconds
                                await Task.Delay(2000);
                                // CORRECT: This will trigger form re-render
                                model.Set(new MyModel("John Doe", "john@example.com", 30));
                            });
                    
                            return Layout.Vertical()
                                | form
                                | (Layout.Horizontal()
                                    | new Button("Update Model", _ => {
                                        model.Set(new MyModel("Jane Doe", "jane@example.com", 25));
                                    })
                                    | new Button("Reset Form", _ => {
                                        model.Set(new MyModel("", "", 0));
                                    }))
                                | Text.Block($"Current: {model.Value.Name} ({model.Value.Email}) - Age: {model.Value.Age}");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("This example works because it uses the form's internal state management. The form maintains its own copy of the data until submission, so programmatic updates using `.Set()` will be reflected in the form fields.", icon:Icons.CircleAlert).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Form submit strategies
                
                Control when form state is committed back to your model by calling `.SubmitStrategy(FormSubmitStrategy.X)` on the form builder. This determines when validation runs and when the bounded state is updated.
                
                | Strategy | When state is committed | Submit button |
                |----------|-------------------------|---------------|
                | `OnSubmit` (default) | Only when the user clicks the submit button or presses Enter | Shown |
                | `OnBlur` | When any field loses focus (tab away or click outside) | Hidden |
                | `OnChange` | On every field value change (keystroke or selection) | Hidden |
                
                **OnSubmit (default)** — Use for traditional forms where the user fills fields and explicitly saves. The examples in [Basic Form Submission](#basic-form-submission) and [Form Submission with State Updates](#form-submission-with-state-updates) use this strategy.
                
                **OnBlur** — Use when you want to commit after the user finishes editing each field, without a submit button:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new OnBlurStrategyExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class OnBlurStrategyExample : ViewBase
                    {
                        public record ProfileModel(string DisplayName, string Bio);
                    
                        public override object? Build()
                        {
                            var profile = UseState(() => new ProfileModel("", ""));
                            var client = UseService<IClientProvider>();
                    
                            UseEffect(() =>
                            {
                                if (!string.IsNullOrEmpty(profile.Value.DisplayName))
                                {
                                    client.Toast($"Profile updated: {profile.Value.DisplayName}");
                                }
                            }, profile);
                    
                            return Layout.Vertical()
                                | profile.ToForm()
                                    .SubmitStrategy(FormSubmitStrategy.OnBlur)
                                    .Label(m => m.DisplayName, "Display Name")
                                    .Label(m => m.Bio, "Bio")
                                | Text.Block($"Submitted: {profile.Value.DisplayName} — {profile.Value.Bio}");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("**OnChange (auto-save)** — Use for settings or preferences where changes should apply immediately:").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new OnChangeStrategyExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class OnChangeStrategyExample : ViewBase
                    {
                        public record SettingsModel(string Name, string Theme, int FontSize);
                    
                        public override object? Build()
                        {
                            var settings = UseState(() => new SettingsModel("Default", "Light", 14));
                            var client = UseService<IClientProvider>();
                    
                            return Layout.Vertical()
                                | settings.ToForm()
                                    .SubmitStrategy(FormSubmitStrategy.OnChange)
                                    .Label(m => m.Name, "Display Name")
                                    .Label(m => m.Theme, "Theme")
                                    .Label(m => m.FontSize, "Font Size")
                                | Text.Block($"Current: {settings.Value.Name}, {settings.Value.Theme}, {settings.Value.FontSize}px");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("Use `OnChange` for settings panels where changes should apply immediately. Use `OnBlur` when you want to commit after the user finishes editing each field without a submit button.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Advanced Features
                
                ### Conditional Fields
                
                Show or hide [fields](app://widgets/inputs/field) based on other field values using `.Visible()`.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ConditionalFieldsExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ConditionalFieldsExample : ViewBase
                    {
                        public record AccountModel(
                            string Email,
                            string Password,
                            bool HasTwoFactor,
                            string PhoneNumber,
                            bool IsBusinessAccount,
                            string CompanyName,
                            string TaxId
                        );
                    
                        public override object? Build()
                        {
                            var account = UseState(() => new AccountModel("", "", false, "", false, "", ""));
                    
                            return account.ToForm()
                                .Visible(m => m.PhoneNumber, m => m.HasTwoFactor)
                                .Visible(m => m.CompanyName, m => m.IsBusinessAccount)
                                .Visible(m => m.TaxId, m => m.IsBusinessAccount)
                                .Label(m => m.Email, "Email Address")
                                .Label(m => m.Password, "Password")
                                .Label(m => m.HasTwoFactor, "Enable Two-Factor Authentication")
                                .Label(m => m.PhoneNumber, "Phone Number (for 2FA)")
                                .Label(m => m.IsBusinessAccount, "Business Account")
                                .Label(m => m.CompanyName, "Company Name")
                                .Label(m => m.TaxId, "Tax ID")
                                .Required(m => m.Email, m => m.Password);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Dynamic Field Configuration
                
                Configure [fields](app://widgets/inputs/field) based on runtime conditions.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DynamicConfigurationExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class DynamicConfigurationExample : ViewBase
                    {
                        public record UserModel(
                            string Username,
                            string Email,
                            string Password,
                            bool IsAdmin,
                            string Role
                        );
                    
                        public override object? Build()
                        {
                            var user = UseState(() => new UserModel("", "", "", false, ""));
                            var isEditMode = UseState(false);
                            var currentUser = UseState(() => new UserModel("admin", "admin@example.com", "", true, "Admin"));
                    
                            // Build the form with conditional field visibility instead of removal
                            var form = user.ToForm()
                                .Visible(m => m.Username, m => !isEditMode.Value) // Hide username in edit mode
                                .Visible(m => m.IsAdmin, m => currentUser.Value.IsAdmin) // Only show admin field to admins
                                .Visible(m => m.Role, m => currentUser.Value.IsAdmin) // Only show role field to admins
                                .Required(m => m.Email, m => m.Password);
                    
                            return Layout.Vertical()
                                | (Layout.Horizontal()
                                    | new Button("New User").OnClick(_ => isEditMode.Set(false))
                                    | new Button("Edit User").OnClick(_ => isEditMode.Set(true)))
                                | form
                                | (Layout.Horizontal()
                                    | new Button(isEditMode.Value ? "Update User" : "Create User")
                                    | new Button("Cancel").Variant(ButtonVariant.Outline));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Forms in UI Components
                
                ### Sheet Forms
                
                Open forms in slide-out [sheets](app://widgets/advanced/sheet) using `.ToSheet()`.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SheetFormExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SheetFormExample : ViewBase
                    {
                        public record ProductModel(
                            string Name,
                            string Description,
                            decimal Price,
                            string Category
                        );
                    
                        public override object? Build()
                        {
                            var product = UseState(() => new ProductModel("", "", 0.0m, ""));
                            var isSheetOpen = UseState(false);
                    
                            return Layout.Vertical()
                                | new Button("Add New Product").OnClick(_ => isSheetOpen.Set(true))
                                | product.ToForm()
                                    .Required(m => m.Name, m => m.Price, m => m.Category)
                                    .ToSheet(isSheetOpen, "New Product", "Fill in the product details below");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Dialog Forms
                
                Open forms in modal [dialogs](app://widgets/common/dialog) using `.ToDialog()`.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DialogFormExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class DialogFormExample : ViewBase
                    {
                        public record UserModel(
                            string FirstName,
                            string LastName,
                            string Email,
                            string Department
                        );
                    
                        public override object? Build()
                        {
                            var user = UseState(() => new UserModel("", "", "", ""));
                            var isDialogOpen = UseState(false);
                    
                            return Layout.Vertical()
                                | new Button("Create User").OnClick(_ => isDialogOpen.Set(true))
                                | user.ToForm()
                                    .Required(m => m.FirstName, m => m.LastName, m => m.Email)
                                    .ToDialog(isDialogOpen, "Create New User", "Please provide user information",
                                             width: Size.Units(125));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Migration from Manual Forms
                
                If you're currently using manual form layouts, migrate to `.ToForm()`:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ❌ Don't do this - manual form layout
                var form = new Form(
                    new TextInput(name),
                    new TextInput(email),
                    new Button("Submit")
                );
                
                // ✅ Do this instead - use .ToForm()
                var model = UseState(() => new UserModel("", ""));
                return model.ToForm()
                    .Required(m => m.Name, m => m.Email);
                """",Languages.Csharp)
            | new Callout("Avoid manually creating form layouts. Always use `.ToForm()` on your [state](app://hooks/core/use-state) objects for better state management, validation, and type safety.", icon:Icons.CircleAlert).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## UseForm
                
                The `UseForm` hook returns a tuple containing form submission handler, form view, validation view, and loading state. It enables manual control over form rendering and submission, which is useful for custom [layouts](app://onboarding/concepts/layout), [dialogs](app://widgets/common/dialog), and [sheets](app://widgets/advanced/sheet) where the form builder's default layout isn't suitable.
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                graph LR
                    A[UseForm Hook] --> B[Create Form Builder]
                    B --> C[Initialize Validation]
                    C --> D[Return Submission Handler]
                    D --> E[Return Form View]
                    E --> F[Return Validation View]
                    F --> G[Return Loading State]
                ```
                """").OnLinkClick(onLinkClick)
            | new Callout("In most cases, you'll use `.ToForm()` directly on your [state](app://hooks/core/use-state) objects for automatic form rendering. Use the `UseForm` hook when you need custom [layouts](app://onboarding/concepts/layout), want to place the form in [dialogs](app://widgets/common/dialog)/[sheets](app://widgets/advanced/sheet), or need manual control over form submission.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Basic Usage
                
                Use `UseForm` when you need manual control over form rendering and submission, such as in [dialogs](app://widgets/common/dialog) or [sheets](app://widgets/advanced/sheet).
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new UseFormHookExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class UseFormHookExample : ViewBase
                    {
                        public record UserModel(string Name, string Email, int Age);
                    
                        public override object? Build()
                        {
                            var user = UseState(() => new UserModel("", "", 25));
                            var (onSubmit, formView, validationView, loading) = UseForm(() => user.ToForm()
                                .Required(m => m.Name, m => m.Email));
                    
                            async ValueTask HandleSubmit()
                            {
                                if (await onSubmit())
                                {
                                    // Form is valid, process the submission
                                    // user.Value contains the validated form data
                                }
                            }
                    
                            return Layout.Vertical()
                                | formView
                                | Layout.Horizontal()
                                    | new Button("Save").OnClick(_ => HandleSubmit())
                                        .Loading(loading).Disabled(loading)
                                    | validationView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Form", "Ivy.FormExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Forms/Form.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Forms with Data Tables",
                Vertical().Gap(4)
                | new Markdown("Combine forms with [data tables](app://widgets/advanced/data-table) for CRUD operations.").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new CrudFormExample())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class CrudFormExample : ViewBase
                        {
                            public record ProductModel(
                                string Name,
                                string Description,
                                decimal Price,
                                string Category
                            );
                        
                            public override object? Build()
                            {
                                var products = UseState(() => new ProductModel[0]);
                                var selectedProduct = UseState<ProductModel?>(() => null);
                                var editingProduct = UseState<ProductModel?>(() => null);
                                var isCreateDialogOpen = UseState(false);
                                var isEditDialogOpen = UseState(false);
                        
                                var addProduct = (Event<Button> e) =>
                                {
                                    editingProduct.Set(new ProductModel("", "", 0.0m, ""));
                                    isCreateDialogOpen.Set(true);
                                };
                        
                                var editProduct = (Event<Button> e) =>
                                {
                                    if (selectedProduct.Value != null)
                                    {
                                        editingProduct.Set(selectedProduct.Value);
                                        isEditDialogOpen.Set(true);
                                    }
                                };
                        
                                var deleteProduct = (Event<Button> e) =>
                                {
                                    if (selectedProduct.Value != null)
                                    {
                                        var updatedProducts = products.Value.Where(p => p != selectedProduct.Value).ToArray();
                                        products.Set(updatedProducts);
                                        selectedProduct.Set((ProductModel?)null);
                                    }
                                };
                        
                                // Create dialog content for new product
                                var createDialog = isCreateDialogOpen.Value && editingProduct.Value != null
                                    ? new Dialog(
                                        _ => isCreateDialogOpen.Set(false),
                                        new DialogHeader("Create New Product"),
                                        new DialogBody(
                                            Layout.Vertical()
                                                | Text.Block("Fill in the product details")
                                                | editingProduct.ToForm()
                                                    .Required(m => m.Name, m => m.Price, m => m.Category)
                                        ),
                                        new DialogFooter(
                                            new Button("Cancel", _ => isCreateDialogOpen.Set(false)).Outline(),
                                            new Button("Create Product", _ => {
                                                if (editingProduct.Value != null)
                                                {
                                                    var updatedProducts = products.Value.Append(editingProduct.Value).ToArray();
                                                    products.Set(updatedProducts);
                                                    isCreateDialogOpen.Set(false);
                                                }
                                            })
                                        )
                                    ).Width(Size.Units(125))
                                    : null;
                        
                                // Create dialog content for editing product
                                var editDialog = isEditDialogOpen.Value && editingProduct.Value != null
                                    ? new Dialog(
                                        _ => isEditDialogOpen.Set(false),
                                        new DialogHeader("Edit Product"),
                                        new DialogBody(
                                            Layout.Vertical()
                                                | Text.Block("Update product information")
                                                | editingProduct.ToForm()
                                                    .Required(m => m.Name, m => m.Price, m => m.Category)
                                        ),
                                        new DialogFooter(
                                            new Button("Cancel", _ => isEditDialogOpen.Set(false)).Outline(),
                                            new Button("Update Product", _ => {
                                                if (editingProduct.Value != null && selectedProduct.Value != null)
                                                {
                                                    var updatedProducts = products.Value.Select(p =>
                                                        p == selectedProduct.Value ? editingProduct.Value : p).ToArray();
                                                    products.Set(updatedProducts);
                                                    selectedProduct.Set(editingProduct.Value);
                                                    isEditDialogOpen.Set(false);
                                                }
                                            })
                                        )
                                    ).Width(Size.Units(125))
                                    : null;
                        
                                return Layout.Vertical()
                                    | (Layout.Horizontal()
                                        | new Button("Add Product", addProduct)
                                        | new Button("Edit Product", editProduct)
                                            .Disabled(selectedProduct.Value == null)
                                        | new Button("Delete Product", deleteProduct)
                                            .Disabled(selectedProduct.Value == null)
                                            .Variant(ButtonVariant.Destructive))
                                    | (Layout.Vertical()
                                        | Text.Block("Select a product to edit/delete:")
                                        | new SelectInput<ProductModel?>(
                                            selectedProduct.Value,
                                            e => selectedProduct.Set(e.Value),
                                            products.Value.ToOptions()
                                        ).Placeholder("Choose a product..."))
                                    | products.Value.ToTable()
                                        .Width(Size.Full())
                                        .Builder(p => p.Name, f => f.Default())
                                        .Builder(p => p.Description, f => f.Text())
                                        .Builder(p => p.Price, f => f.Default())
                                        .Builder(p => p.Category, f => f.Default())
                                    | (selectedProduct.Value != null ?
                                        Text.Block($"Selected: {selectedProduct.Value.Name} (${selectedProduct.Value.Price})")
                                        : Text.Block("No product selected"))
                                    | createDialog!
                                    | editDialog!;
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("Forms with Real-time Updates",
                Vertical().Gap(4)
                | new Markdown("Forms automatically update state, enabling real-time UI updates.").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new RealTimeFormExample())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class RealTimeFormExample : ViewBase
                        {
                            public record CalculatorModel(
                                decimal Number1,
                                decimal Number2,
                                string Operation
                            );
                        
                            public override object? Build()
                            {
                                var calculator = UseState(() => new CalculatorModel(0, 0, "+"));
                        
                                decimal CalculateResult()
                                {
                                    return calculator.Value.Operation switch
                                    {
                                        "+" => calculator.Value.Number1 + calculator.Value.Number2,
                                        "-" => calculator.Value.Number1 - calculator.Value.Number2,
                                        "*" => calculator.Value.Number1 * calculator.Value.Number2,
                                        "/" => calculator.Value.Number2 != 0 ? calculator.Value.Number1 / calculator.Value.Number2 : 0,
                                        _ => 0
                                    };
                                }
                        
                                // Create options for the operation select input
                                var operationOptions = new[] { "+", "-", "*", "/" }.ToOptions();
                        
                                return Layout.Horizontal()
                                    | new Card(
                                        calculator.ToForm()
                                            .Builder(m => m.Operation, s => s.ToSelectInput(operationOptions))
                                            .Label(m => m.Number1, "First Number")
                                            .Label(m => m.Number2, "Second Number")
                                            .Label(m => m.Operation, "Operation")
                                    ).Title("Calculator").Width(Size.Fraction(1 / 2f))
                                    | new Card(
                                        Layout.Vertical()
                                            | Text.H3("Result")
                                            | Text.Block($"{calculator.Value.Number1} {calculator.Value.Operation} {calculator.Value.Number2} = {CalculateResult()}")
                                    ).Title("Result").Width(Size.Fraction(1 / 2f));
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.Core.UseStateApp), typeof(Widgets.Inputs.FieldApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Widgets.Inputs.TextInputApp), typeof(Widgets.Inputs.BoolInputApp), typeof(Widgets.Inputs.NumberInputApp), typeof(Widgets.Inputs.DateTimeInputApp), typeof(Widgets.Inputs.SelectInputApp), typeof(Widgets.Inputs.ReadOnlyInputApp), typeof(Onboarding.Concepts.LayoutApp), typeof(Hooks.Core.UseEffectApp), typeof(Widgets.Advanced.SheetApp), typeof(Widgets.Common.DialogApp)]; 
        return article;
    }
}


public class BasicFormExample : ViewBase
{
    public record UserModel(string Name, string Email, bool IsActive, int Age);

    public override object? Build()
    {
        var user = UseState(() => new UserModel("", "", false, 25));
        
        return user.ToForm();
    }
}

public class ConfiguredFormExample : ViewBase
{
    public record ContactModel(
        string Name,
        string Email,
        string Phone,
        string Message,
        bool Subscribe,
        Gender Gender = Gender.Other
    );

    public enum Gender { Male, Female, Other }

    public override object? Build()
    {
        var contact = UseState(() => new ContactModel("", "", "", "", false));
        
        return contact.ToForm()
            .Label(m => m.Name, "Full Name")
            .Description(m => m.Name, "Enter your full name as it appears on official documents")
            .Label(m => m.Email, "Email Address")
            .Help(m => m.Email, "We'll use this email to send you updates and important notifications")
            .Description(m => m.Email, "We'll use this to send you updates")
            .Label(m => m.Phone, "Phone Number")
            .Label(m => m.Message, "Your Message")
            .Required(m => m.Name, m => m.Email);
    }
}

public class CustomInputsExample : ViewBase
{
    public record ProductModel(
        string Name,
        string Description,
        decimal Price,
        string JsonConfig,
        List<string> Tags,
        DateTime ReleaseDate
    );

    public override object? Build()
    {
        var product = UseState(() => new ProductModel("", "", 0.0m, "{}", new(), DateTime.Now));
        
        // Create sample tag options for the multi-select
        var tagOptions = new[] { "Electronics", "Clothing", "Books", "Home", "Sports", "Food" }.ToOptions();
        
        return product.ToForm()
            .Builder(m => m.Description, s => s.ToTextareaInput())
            .Builder(m => m.JsonConfig, s => s.ToCodeInput().Language(Languages.Json))
            .Builder(m => m.Tags, s => s.ToSelectInput(tagOptions))
            .Builder(m => m.ReleaseDate, s => s.ToDateTimeInput())
            .Label(m => m.Description, "Product Description")
            .Label(m => m.JsonConfig, "Configuration (JSON)")
            .Label(m => m.Tags, "Product Tags")
            .Label(m => m.ReleaseDate, "Release Date");
    }
}

public class RequiredFieldsExample : ViewBase
{
    public record OrderModel(
        string CustomerName,
        string? CustomerEmail, 
        string ShippingAddress,
        int Quantity,
        bool IsPriority
    );

    public override object? Build()
    {
        var order = UseState(() => new OrderModel("", null, "", 1, false));
        
        return order.ToForm()
            .Required(m => m.CustomerEmail) 
            .Required(m => m.IsPriority)    
            .Label(m => m.CustomerName, "Customer Name")
            .Label(m => m.CustomerEmail, "Email Address")
            .Label(m => m.ShippingAddress, "Shipping Address")
            .Help(m => m.ShippingAddress, "Enter the complete shipping address including street, city, and postal code")
            .Label(m => m.Quantity, "Quantity")
            .Label(m => m.IsPriority, "Priority Order");
    }
}

public class LayoutControlExample : ViewBase
{
    public record AddressModel(
        string Street,
        string City,
        string State,
        string ZipCode,
        string Country
    );

    public override object? Build()
    {
        var address = UseState(() => new AddressModel("", "", "", "", ""));
        
        return address.ToForm()
            .Place(m => m.Street)                    // Single field spans full width
            .PlaceHorizontal(m => m.City, m => m.State)  // Two fields side-by-side, sharing row width
            .PlaceHorizontal(m => m.ZipCode, m => m.Country) // Two fields side-by-side, sharing row width
            .Label(m => m.Street, "Street Address")
            .Label(m => m.City, "City")
            .Label(m => m.State, "State/Province")
            .Label(m => m.ZipCode, "ZIP/Postal Code")
            .Label(m => m.Country, "Country");
    }
}

public class GroupedFieldsExample : ViewBase
{
    public record EmployeeModel(
        string FirstName,
        string LastName,
        string Email,
        string Department,
        decimal Salary,
        DateTime HireDate,
        string Street,
        string City,
        string State
    );

    public override object? Build()
    {
        var employee = UseState(() => new EmployeeModel("", "", "", "", 0.0m, DateTime.Now, "", "", ""));
        
        return employee.ToForm()
            .Group("Personal Information", m => m.FirstName, m => m.LastName, m => m.Email)
            .Group("Employment", m => m.Department, m => m.Salary, m => m.HireDate)
            .Group("Address", m => m.Street, m => m.City, m => m.State)
            .Label(m => m.FirstName, "First Name")
            .Label(m => m.LastName, "Last Name")
            .Label(m => m.Email, "Email Address")
            .Label(m => m.Department, "Department")
            .Label(m => m.Salary, "Annual Salary")
            .Label(m => m.HireDate, "Hire Date")
            .Label(m => m.Street, "Street Address")
            .Label(m => m.City, "City")
            .Label(m => m.State, "State");
    }
}

public class FieldManagementExample : ViewBase
{
    public record ProductModel(
        string Name,
        string Description,
        decimal Price,
        string Category,
        int Stock,
        string SKU,
        DateTime CreatedDate
    );

    public override object? Build()
    {
        var product = UseState(() => new ProductModel("", "", 0.0m, "", 0, "", DateTime.Now));
        
        return product.ToForm()
            .Clear()                                    // Hide all auto-generated fields
            .Add(m => m.Name)                          // Show Name first
            .Add(m => m.Description)                   // Show Description second
            .Add(m => m.Price)                         // Show Price third
            .Add(m => m.Category)                      // Show Category fourth
            .Add(m => m.Stock)                         // Show Stock last
            .Remove(m => m.SKU)                        // Hide SKU field
            .Remove(m => m.CreatedDate)                // Hide CreatedDate field
            .Label(m => m.Name, "Product Name")
            .Label(m => m.Description, "Product Description")
            .Label(m => m.Price, "Unit Price")
            .Label(m => m.Category, "Product Category")
            .Label(m => m.Stock, "Available Stock");
    }
}

public class CustomSubmitTitleFormExample : ViewBase
{
    public record UserModel(string Name, string Email, bool IsActive, int Age);

    public override object? Build()
    {
        var user = UseState(() => new UserModel("", "", false, 25));
        
        return user.ToForm("Create new user");
    }
}

public class ValidationExample : ViewBase
{
    public class UserModel
    {
        [Required(ErrorMessage = "Username is required")]
        [Length(3, 50, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string Username { get; set; } = "";
        
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [MaxLength(100)]
        public string Email { get; set; } = "";
        
        [Required]
        [Length(8, 100, ErrorMessage = "Password must be between 8 and 100 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
        
        [Range(13, 120, ErrorMessage = "Age must be between 13 and 120")]
        public int Age { get; set; } = 18;
        
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string? PhoneNumber { get; set; }
        
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string? Website { get; set; }
        
        [AllowedValues("USA", "Canada", "UK", ErrorMessage = "Please select a valid country")]
        public string Country { get; set; } = "USA";
        
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "ZIP code must be in format 12345 or 12345-6789")]
        public string? ZipCode { get; set; }
        
        [CreditCard(ErrorMessage = "Please enter a valid credit card number")]
        [Length(13, 19, ErrorMessage = "Credit card number must be between 13 and 19 digits")]
        public string? CreditCardNumber { get; set; }
        
        public DateTime BirthDate { get; set; } = DateTime.Now;
    }

    public override object? Build()
    {
        var user = UseState(() => new UserModel());
        var client = UseService<IClientProvider>();
        
        UseEffect(() =>
        {
            // This only fires when the form is submitted successfully (passes validation)
            if (!string.IsNullOrEmpty(user.Value.Username))
            {
                client.Toast($"Account created for {user.Value.Username}!");
            }
        }, user);
        
        return user.ToForm("Create Account")
            // Custom validation: birth date cannot be in the future
            .Validate<DateTime>(m => m.BirthDate, birthDate => 
                (birthDate <= DateTime.Now, "Birth date cannot be in the future"))
            // Custom validation: username cannot contain spaces
            .Validate<string>(m => m.Username, username =>
                (!username.Contains(' '), "Username cannot contain spaces"));
    }
}

public class DisplayAttributeExample : ViewBase
{
    public class UserRegistrationModel
    {
        [Display(Name = "Full Name", Description = "Enter your complete legal name", Order = 1)]
        [Required(ErrorMessage = "Full name is required")]
        [Length(2, 100)]
        public string Name { get; set; } = "";

        [Display(Name = "Email Address", Description = "We'll use this for account verification", Order = 2)]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Display(Name = "Phone Number", Description = "For account security purposes", Prompt = "+1-234-567-8900", Order = 3)]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(GroupName = "Account Security", Name = "Password", Order = 4)]
        [Required]
        [Length(8, 100)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Display(GroupName = "Account Security", Name = "Confirm Password", Order = 5)]
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = "";

        [Display(GroupName = "Preferences", Name = "Newsletter Subscription", Description = "Receive weekly updates", Order = 6)]
        public bool SubscribeToNewsletter { get; set; } = false;

        [Display(GroupName = "Preferences", Name = "Preferred Theme", Order = 7)]
        [AllowedValues("Light", "Dark", "Auto")]
        public string Theme { get; set; } = "Auto";
    }

    public override object? Build()
    {
        var user = UseState(() => new UserRegistrationModel());
        var client = UseService<IClientProvider>();
        
        UseEffect(() =>
        {
            if (!string.IsNullOrEmpty(user.Value.Name))
            {
                client.Toast($"Registration completed for {user.Value.Name}!");
            }
        }, user);

        return user.ToForm("Create Account")
            .Builder(m => m.Password, s => s.ToPasswordInput())
            .Builder(m => m.ConfirmPassword, s => s.ToPasswordInput())
            .Validate<string>(m => m.ConfirmPassword, confirmPassword =>
                (confirmPassword == user.Value.Password, "Passwords must match"));
    }
}

public class ProgrammaticValidationExample : ViewBase
{
    public record ProductModel(
        string Name,
        string? Description,
        decimal Price,
        int Stock
    );

    public override object? Build()
    {
        var product = UseState(() => new ProductModel("", null, 0.0m, 0));
        
        return product.ToForm()
            // Mark fields as required programmatically
            .Required(m => m.Name, m => m.Price)
            // Add custom validation logic
            .Validate<string>(m => m.Name, name =>
                (name.Length >= 3, "Product name must be at least 3 characters"))
            .Validate<decimal>(m => m.Price, price =>
                (price > 0, "Price must be greater than zero"))
            .Validate<int>(m => m.Stock, stock =>
                (stock >= 0, "Stock cannot be negative"))
            .Label(m => m.Name, "Product Name")
            .Label(m => m.Description, "Description")
            .Label(m => m.Price, "Price")
            .Label(m => m.Stock, "Stock Quantity");
    }
}

public class CollectionValidationExample : ViewBase
{
    public class SurveyModel
    {
        [Display(Name = "Interests", Description = "Select at least one interest")]
        [Required(ErrorMessage = "Please select at least one interest")]
        [MinLength(1, ErrorMessage = "You must select at least one interest")]
        [AllowedValues("Technology", "Sports", "Music", "Art", "Travel")]
        public string[] Interests { get; set; } = Array.Empty<string>();
        
        [Display(Name = "Tags")]
        [Length(1, 5, ErrorMessage = "Select between 1 and 5 tags")]
        public List<string> Tags { get; set; } = new();
    }

    public override object? Build()
    {
        var survey = UseState(() => new SurveyModel());
        var tagOptions = new[] { "New", "Popular", "Featured", "Sale", "Limited" }.ToOptions();

        return survey.ToForm()
            .Builder(m => m.Tags, s => s.ToSelectInput(tagOptions).List());
    }
}

public class MultipleValidatorsExample : ViewBase
{
    public class AccountModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        [Length(3, 20, ErrorMessage = "Username must be between 3 and 20 characters")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores")]
        public string Username { get; set; } = "";
        
        [Display(Name = "Email")]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = "";
        
        [Display(Name = "Password")]
        [Required]
        [Length(8, 128, ErrorMessage = "Password must be between 8 and 128 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)", ErrorMessage = "Password must contain uppercase, lowercase, and a number")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
    }

    public override object? Build()
    {
        var account = UseState(() => new AccountModel());
        
        return account.ToForm("Create Account")
            .Builder(m => m.Password, s => s.ToPasswordInput());
    }
}

public class FormSubmissionExample : ViewBase
{
    public record ContactModel(string Name, string Email, string Message);

    public override object? Build()
    {
        var contact = UseState(() => new ContactModel("", "", ""));
        var client = UseService<IClientProvider>();
        
        UseEffect(() =>
        {
            if (!string.IsNullOrEmpty(contact.Value.Name))
            {
                client.Toast($"Message from {contact.Value.Name} sent successfully!");
            }
        }, contact);
        
        return contact.ToForm()
            .Required(m => m.Name, m => m.Email, m => m.Message);
    }
}

public class FormStatesExample : ViewBase
{
    public record OrderModel(
        string CustomerName,
        string ProductName,
        int Quantity,
        decimal UnitPrice
    );

    public override object? Build()
    {
        var order = UseState(() => new OrderModel("", "", 1, 0.0m));
        var client = UseService<IClientProvider>();
        
        UseEffect(() =>
        {
            if (!string.IsNullOrEmpty(order.Value.CustomerName))
            {
                var total = order.Value.Quantity * order.Value.UnitPrice;
                client.Toast($"Order created! Total: ${total:F2}");
            }
        }, order);
        
        return Layout.Vertical()
            | order.ToForm()
                .Required(m => m.CustomerName, m => m.ProductName, m => m.Quantity, m => m.UnitPrice)
            | Text.Block($"Total: ${order.Value.Quantity * order.Value.UnitPrice:F2}");
    }
}

public class SimpleFormWithResetExample : ViewBase
{
    public record MyModel(string Name, string Email, int Age);

    public override object? Build()
    {
        // Create the state for your model
        var model = UseState(() => new MyModel("", "", 0));
        
        // Create a form from the state
        var form = model.ToForm()
            .Label(m => m.Name, "Full Name")
            .Label(m => m.Email, "Email Address")
            .Label(m => m.Age, "Age");
        
        // To update the model and trigger re-render, you MUST use Set()
        UseEffect(async () =>
        {
            // Example: Load data after 2 seconds
            await Task.Delay(2000);
            // CORRECT: This will trigger form re-render
            model.Set(new MyModel("John Doe", "john@example.com", 30));
        });
        
        return Layout.Vertical()
            | form
            | (Layout.Horizontal()
                | new Button("Update Model", _ => {
                    model.Set(new MyModel("Jane Doe", "jane@example.com", 25));
                })
                | new Button("Reset Form", _ => {
                    model.Set(new MyModel("", "", 0));
                }))
            | Text.Block($"Current: {model.Value.Name} ({model.Value.Email}) - Age: {model.Value.Age}");
    }
}

public class OnBlurStrategyExample : ViewBase
{
    public record ProfileModel(string DisplayName, string Bio);

    public override object? Build()
    {
        var profile = UseState(() => new ProfileModel("", ""));
        var client = UseService<IClientProvider>();

        UseEffect(() =>
        {
            if (!string.IsNullOrEmpty(profile.Value.DisplayName))
            {
                client.Toast($"Profile updated: {profile.Value.DisplayName}");
            }
        }, profile);

        return Layout.Vertical()
            | profile.ToForm()
                .SubmitStrategy(FormSubmitStrategy.OnBlur)
                .Label(m => m.DisplayName, "Display Name")
                .Label(m => m.Bio, "Bio")
            | Text.Block($"Submitted: {profile.Value.DisplayName} — {profile.Value.Bio}");
    }
}

public class OnChangeStrategyExample : ViewBase
{
    public record SettingsModel(string Name, string Theme, int FontSize);

    public override object? Build()
    {
        var settings = UseState(() => new SettingsModel("Default", "Light", 14));
        var client = UseService<IClientProvider>();

        return Layout.Vertical()
            | settings.ToForm()
                .SubmitStrategy(FormSubmitStrategy.OnChange)
                .Label(m => m.Name, "Display Name")
                .Label(m => m.Theme, "Theme")
                .Label(m => m.FontSize, "Font Size")
            | Text.Block($"Current: {settings.Value.Name}, {settings.Value.Theme}, {settings.Value.FontSize}px");
    }
}

public class ConditionalFieldsExample : ViewBase
{
    public record AccountModel(
        string Email,
        string Password,
        bool HasTwoFactor,
        string PhoneNumber,
        bool IsBusinessAccount,
        string CompanyName,
        string TaxId
    );

    public override object? Build()
    {
        var account = UseState(() => new AccountModel("", "", false, "", false, "", ""));
        
        return account.ToForm()
            .Visible(m => m.PhoneNumber, m => m.HasTwoFactor)
            .Visible(m => m.CompanyName, m => m.IsBusinessAccount)
            .Visible(m => m.TaxId, m => m.IsBusinessAccount)
            .Label(m => m.Email, "Email Address")
            .Label(m => m.Password, "Password")
            .Label(m => m.HasTwoFactor, "Enable Two-Factor Authentication")
            .Label(m => m.PhoneNumber, "Phone Number (for 2FA)")
            .Label(m => m.IsBusinessAccount, "Business Account")
            .Label(m => m.CompanyName, "Company Name")
            .Label(m => m.TaxId, "Tax ID")
            .Required(m => m.Email, m => m.Password);
    }
}

public class DynamicConfigurationExample : ViewBase
{
    public record UserModel(
        string Username,
        string Email,
        string Password,
        bool IsAdmin,
        string Role
    );

    public override object? Build()
    {
        var user = UseState(() => new UserModel("", "", "", false, ""));
        var isEditMode = UseState(false);
        var currentUser = UseState(() => new UserModel("admin", "admin@example.com", "", true, "Admin"));
        
        // Build the form with conditional field visibility instead of removal
        var form = user.ToForm()
            .Visible(m => m.Username, m => !isEditMode.Value) // Hide username in edit mode
            .Visible(m => m.IsAdmin, m => currentUser.Value.IsAdmin) // Only show admin field to admins
            .Visible(m => m.Role, m => currentUser.Value.IsAdmin) // Only show role field to admins
            .Required(m => m.Email, m => m.Password);
        
        return Layout.Vertical()
            | (Layout.Horizontal()
                | new Button("New User").OnClick(_ => isEditMode.Set(false))
                | new Button("Edit User").OnClick(_ => isEditMode.Set(true)))
            | form
            | (Layout.Horizontal()
                | new Button(isEditMode.Value ? "Update User" : "Create User")
                | new Button("Cancel").Variant(ButtonVariant.Outline));
    }
}

public class SheetFormExample : ViewBase
{
    public record ProductModel(
        string Name,
        string Description,
        decimal Price,
        string Category
    );

    public override object? Build()
    {
        var product = UseState(() => new ProductModel("", "", 0.0m, ""));
        var isSheetOpen = UseState(false);
        
        return Layout.Vertical()
            | new Button("Add New Product").OnClick(_ => isSheetOpen.Set(true))
            | product.ToForm()
                .Required(m => m.Name, m => m.Price, m => m.Category)
                .ToSheet(isSheetOpen, "New Product", "Fill in the product details below");
    }
}

public class DialogFormExample : ViewBase
{
    public record UserModel(
        string FirstName,
        string LastName,
        string Email,
        string Department
    );

    public override object? Build()
    {
        var user = UseState(() => new UserModel("", "", "", ""));
        var isDialogOpen = UseState(false);
        
        return Layout.Vertical()
            | new Button("Create User").OnClick(_ => isDialogOpen.Set(true))
            | user.ToForm()
                .Required(m => m.FirstName, m => m.LastName, m => m.Email)
                .ToDialog(isDialogOpen, "Create New User", "Please provide user information", 
                         width: Size.Units(125));
    }
}

public class UseFormHookExample : ViewBase
{
    public record UserModel(string Name, string Email, int Age);

    public override object? Build()
    {
        var user = UseState(() => new UserModel("", "", 25));
        var (onSubmit, formView, validationView, loading) = UseForm(() => user.ToForm()
            .Required(m => m.Name, m => m.Email));

        async ValueTask HandleSubmit()
        {
            if (await onSubmit())
            {
                // Form is valid, process the submission
                // user.Value contains the validated form data
            }
        }

        return Layout.Vertical()
            | formView
            | Layout.Horizontal()
                | new Button("Save").OnClick(_ => HandleSubmit())
                    .Loading(loading).Disabled(loading)
                | validationView;
    }
}

public class CrudFormExample : ViewBase
{
    public record ProductModel(
        string Name,
        string Description,
        decimal Price,
        string Category
    );

    public override object? Build()
    {
        var products = UseState(() => new ProductModel[0]);
        var selectedProduct = UseState<ProductModel?>(() => null);
        var editingProduct = UseState<ProductModel?>(() => null);
        var isCreateDialogOpen = UseState(false);
        var isEditDialogOpen = UseState(false);
        
        var addProduct = (Event<Button> e) =>
        {
            editingProduct.Set(new ProductModel("", "", 0.0m, ""));
            isCreateDialogOpen.Set(true);
        };
        
        var editProduct = (Event<Button> e) =>
        {
            if (selectedProduct.Value != null)
            {
                editingProduct.Set(selectedProduct.Value);
                isEditDialogOpen.Set(true);
            }
        };
        
        var deleteProduct = (Event<Button> e) =>
        {
            if (selectedProduct.Value != null)
            {
                var updatedProducts = products.Value.Where(p => p != selectedProduct.Value).ToArray();
                products.Set(updatedProducts);
                selectedProduct.Set((ProductModel?)null);
            }
        };
        
        // Create dialog content for new product
        var createDialog = isCreateDialogOpen.Value && editingProduct.Value != null
            ? new Dialog(
                _ => isCreateDialogOpen.Set(false),
                new DialogHeader("Create New Product"),
                new DialogBody(
                    Layout.Vertical()
                        | Text.Block("Fill in the product details")
                        | editingProduct.ToForm()
                            .Required(m => m.Name, m => m.Price, m => m.Category)
                ),
                new DialogFooter(
                    new Button("Cancel", _ => isCreateDialogOpen.Set(false)).Outline(),
                    new Button("Create Product", _ => {
                        if (editingProduct.Value != null)
                        {
                            var updatedProducts = products.Value.Append(editingProduct.Value).ToArray();
                            products.Set(updatedProducts);
                            isCreateDialogOpen.Set(false);
                        }
                    })
                )
            ).Width(Size.Units(125))
            : null;
        
        // Create dialog content for editing product
        var editDialog = isEditDialogOpen.Value && editingProduct.Value != null
            ? new Dialog(
                _ => isEditDialogOpen.Set(false),
                new DialogHeader("Edit Product"),
                new DialogBody(
                    Layout.Vertical()
                        | Text.Block("Update product information")
                        | editingProduct.ToForm()
                            .Required(m => m.Name, m => m.Price, m => m.Category)
                ),
                new DialogFooter(
                    new Button("Cancel", _ => isEditDialogOpen.Set(false)).Outline(),
                    new Button("Update Product", _ => {
                        if (editingProduct.Value != null && selectedProduct.Value != null)
                        {
                            var updatedProducts = products.Value.Select(p => 
                                p == selectedProduct.Value ? editingProduct.Value : p).ToArray();
                            products.Set(updatedProducts);
                            selectedProduct.Set(editingProduct.Value);
                            isEditDialogOpen.Set(false);
                        }
                    })
                )
            ).Width(Size.Units(125))
            : null;
        
        return Layout.Vertical()
            | (Layout.Horizontal()
                | new Button("Add Product", addProduct)
                | new Button("Edit Product", editProduct)
                    .Disabled(selectedProduct.Value == null)
                | new Button("Delete Product", deleteProduct)
                    .Disabled(selectedProduct.Value == null)
                    .Variant(ButtonVariant.Destructive))
            | (Layout.Vertical()
                | Text.Block("Select a product to edit/delete:")
                | new SelectInput<ProductModel?>(
                    selectedProduct.Value,
                    e => selectedProduct.Set(e.Value),
                    products.Value.ToOptions()
                ).Placeholder("Choose a product..."))
            | products.Value.ToTable()
                .Width(Size.Full())
                .Builder(p => p.Name, f => f.Default())
                .Builder(p => p.Description, f => f.Text())
                .Builder(p => p.Price, f => f.Default())
                .Builder(p => p.Category, f => f.Default())
            | (selectedProduct.Value != null ? 
                Text.Block($"Selected: {selectedProduct.Value.Name} (${selectedProduct.Value.Price})")
                : Text.Block("No product selected"))
            | createDialog!
            | editDialog!;
    }
}

public class RealTimeFormExample : ViewBase
{
    public record CalculatorModel(
        decimal Number1,
        decimal Number2,
        string Operation
    );

    public override object? Build()
    {
        var calculator = UseState(() => new CalculatorModel(0, 0, "+"));
        
        decimal CalculateResult()
        {
            return calculator.Value.Operation switch
            {
                "+" => calculator.Value.Number1 + calculator.Value.Number2,
                "-" => calculator.Value.Number1 - calculator.Value.Number2,
                "*" => calculator.Value.Number1 * calculator.Value.Number2,
                "/" => calculator.Value.Number2 != 0 ? calculator.Value.Number1 / calculator.Value.Number2 : 0,
                _ => 0
            };
        }
        
        // Create options for the operation select input
        var operationOptions = new[] { "+", "-", "*", "/" }.ToOptions();
        
        return Layout.Horizontal()
            | new Card(
                calculator.ToForm()
                    .Builder(m => m.Operation, s => s.ToSelectInput(operationOptions))
                    .Label(m => m.Number1, "First Number")
                    .Label(m => m.Number2, "Second Number")
                    .Label(m => m.Operation, "Operation")
            ).Title("Calculator").Width(Size.Fraction(1 / 2f))
            | new Card(
                Layout.Vertical()
                    | Text.H3("Result")
                    | Text.Block($"{calculator.Value.Number1} {calculator.Value.Operation} {calculator.Value.Number2} = {CalculateResult()}")
            ).Title("Result").Width(Size.Fraction(1 / 2f));
    }
}

namespace Application.Services.Operations.Customers.Exceptions
{

    public static class CustomerErrorsMessagesException
    {
        //Adding Product
         public static readonly string AvailableQuantity = "300.0|Available quantity cannot be less than 1.";
         public static readonly string ProductStatus = "300.1|status should be available.";
         //Updating Product
         public static readonly string AddProductWarranty = "300.4|when adding a product warranty cannot be longer than five years.";
         public static readonly string UpdateProductWarrantyEnd1 = "300.15|WarrantyEnd field cannot be longer than or equal current field EntryDate.";
         public static readonly string UpdateProductWarrantyEnd2 = "300.16|WarrantyEnd field cannot be longer than five years from field EntryDate.";
         public static readonly string UpdateProductEntryDate1 = "300.17|Product Entrydate field cannot be empty.";
         public static readonly string UpdateProductEntryDate2 = "300.18|Product EntryDate field, it cannot be less than 15 days from the EntryDate field.";
    }
}















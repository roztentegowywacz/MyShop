namespace MyShop.Core.Domain.Exceptions
{
    public enum ErrorCodes
    {
        [ErrorMessage("Given order status not exists.")]
        bad_order_status,

        [ErrorMessage("Cannot approve already approved order.")]
        cannot_approve_approved_order,

        [ErrorMessage("Cannot approve already canceled order.")]
        cannot_approve_canceled_order,

        [ErrorMessage("Cannot approve a completed order.")]
        cannot_approve_completed_order,

        [ErrorMessage("Cannot approve a revoked order.")]
        cannot_approve_revoked_order,

        [ErrorMessage("Cannot cancel an already canceled order.")]
        cannot_cancel_canceled_order,

        [ErrorMessage("Cannot cancel a completed order.")]
        cannot_cancel_completed_order,

        [ErrorMessage("Cannot cancel a revoked order.")]
        cannot_cancel_revoked_order,

        [ErrorMessage("Cannot complete a canceled order.")]
        cannot_complete_canceled_order,

        [ErrorMessage("Cannot complete an already completed order.")]
        cannot_complete_completed_order,

        [ErrorMessage("Cannot complete not approved order.")]
        cannot_complete_not_approved_order,

        [ErrorMessage("Cannot complete a revoked order.")]
        cannot_complete_revoked_order,

        [ErrorMessage("Can not create an empty order.")]
        cannot_create_empty_order,

        [ErrorMessage("Cannot revoke an already revoked order.")]
        cannot_revoke_revoked_order,

        [ErrorMessage("Cart was not found.")]
        cart_not_found,

        [ErrorMessage("Customer was not found.")]
        customer_not_found,

        [ErrorMessage("Product name cannot be empty.")]
        empty_product_name,
        [ErrorMessage("Product vendor cannot be empty.")]
        empty_product_vendor,
        [ErrorMessage("Product description cannot be empty.")]
        empty_product_description,
        [ErrorMessage("Product price cannot be zero or negative.")]
        invalid_product_price,
        [ErrorMessage("Product quantity cannot be negative.")]
        invalid_product_quantity,
        [ErrorMessage("Product is aleready deleted.")]
        product_deleted,

        [ErrorMessage("Given email is invalid.")]
        invalid_email,

        [ErrorMessage("Given role is invalid.")]
        invalid_role,

        [ErrorMessage("Password can not be empty.")]
        invalid_password,

        [ErrorMessage("Quantity can not be greater than actula quantity.")]
        invalid_quantity,

        [ErrorMessage("Quantity can not be negative.")]
        negative_quantity,

        [ErrorMessage("Not enough products in stock.")]
        not_enough_products_in_stock,

        [ErrorMessage("Order was not found.")]
        order_not_found,

        [ErrorMessage("Product is deleted.")]
        product_is_deleted,

        [ErrorMessage("Product was not found.")]
        product_not_found,

        [ErrorMessage("Refresh token was not found.")]
        refresh_token_not_found,

        [ErrorMessage("User was not found.")]
        user_not_found
    }
}
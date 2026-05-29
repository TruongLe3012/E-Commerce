import { useContext, useState } from "react";

import { CartContext } from "../context/CartContext";

import { createOrder } from "../services/orderService";

function CheckoutPage() {
  const {
    cartItems,
    clearCart,
  } = useContext(CartContext);

  const [loading, setLoading] = useState(false);

  const totalPrice = cartItems.reduce(
    (total, item) =>
      total + item.price * item.quantity,
    0
  );

  const handleCheckout = async () => {
    try {
      setLoading(true);

      const orderData = {
        items: cartItems.map((item) => ({
          productId: item.id,
          quantity: item.quantity,
        })),
      };

      await createOrder(orderData);

      clearCart();

      alert("Order created successfully");
    } catch (error) {
      console.log(error);

      alert("Checkout failed");
    } finally {
      setLoading(false);
    }
  };

  if (cartItems.length === 0) {
    return (
      <h1 className="text-3xl font-bold">
        Cart is empty
      </h1>
    );
  }

  return (
    <div className="max-w-5xl mx-auto">
      <h1 className="text-3xl font-bold mb-6">
        Checkout
      </h1>

      <div className="space-y-4">
        {cartItems.map((item) => (
          <div
            key={item.id}
            className="border p-4 rounded flex justify-between"
          >
            <div>
              <h2 className="font-bold">
                {item.name}
              </h2>

              <p>
                ${item.price} x {item.quantity}
              </p>
            </div>

            <div>
              ${(item.price * item.quantity).toFixed(2)}
            </div>
          </div>
        ))}
      </div>

      <div className="mt-8">
        <h2 className="text-2xl font-bold">
          Total: ${totalPrice.toFixed(2)}
        </h2>

        <button
          onClick={handleCheckout}
          disabled={loading}
          className="mt-4 bg-blue-500 text-white px-6 py-3 rounded"
        >
          {loading
            ? "Processing..."
            : "Place Order"}
        </button>
      </div>
    </div>
  );
}

export default CheckoutPage;
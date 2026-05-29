import { useContext } from "react";

import { Link } from "react-router-dom";

import { CartContext } from "../context/CartContext";

function CartPage() {
  const { cartItems, removeFromCart } =
    useContext(CartContext);

  const totalPrice = cartItems.reduce(
    (total, item) =>
      total + item.price * item.quantity,
    0
  );

  if (cartItems.length === 0) {
    return (
      <h1 className="text-3xl font-bold">
        Your cart is empty
      </h1>
    );
  }

  return (
    <div className="max-w-5xl mx-auto p-6">
      <h1 className="text-3xl font-bold mb-6">
        Cart
      </h1>

      <div className="space-y-4">
        {cartItems.map((item) => (
          <div
            key={item.id}
            className="border p-4 rounded-xl flex justify-between items-center shadow"
          >
            <div>
              <h2 className="font-bold text-lg">
                {item.name}
              </h2>

              <p>${item.price}</p>

              <p>
                Qty: {item.quantity}
              </p>
            </div>

            <button
              onClick={() =>
                removeFromCart(item.id)
              }
              className="bg-red-500 hover:bg-red-600 text-white px-4 py-2 rounded-lg transition"
            >
              Remove
            </button>
          </div>
        ))}
      </div>

      <div className="mt-8">
        <h2 className="text-2xl font-bold">
          Total: ${totalPrice}
        </h2>

        <Link
          to="/checkout"
          className="inline-block mt-4 bg-green-500 hover:bg-green-600 text-white px-6 py-3 rounded-lg transition"
        >
          Checkout
        </Link>
      </div>
    </div>
  );
}

export default CartPage;
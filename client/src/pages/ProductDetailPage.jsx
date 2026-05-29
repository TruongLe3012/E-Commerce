import {
  useContext,
  useEffect,
  useState,
} from "react";

import { useParams } from "react-router-dom";

import { CartContext } from "../context/CartContext";

import { getProductById } from "../services/productService";

function ProductDetailPage() {
  const { id } = useParams();

  const [product, setProduct] = useState(null);

  const { addToCart } = useContext(CartContext);

  useEffect(() => {
    fetchProduct();
  }, [id]);

  const fetchProduct = async () => {
    try {
      const data = await getProductById(id);

      setProduct(data.data || data);
    } catch (error) {
      console.log(error);
    }
  };

  if (!product) {
    return (
      <div className="flex justify-center items-center h-96">
        <h1 className="text-2xl font-bold">
          Loading...
        </h1>
      </div>
    );
  }

  return (
    <div className="max-w-5xl mx-auto grid md:grid-cols-2 gap-10 p-6">
      <img
        src={
          product.imageUrl ||
          "https://via.placeholder.com/500"
        }
        alt={product.name}
        className="w-full rounded-xl shadow"
      />

      <div>
        <h1 className="text-4xl font-bold">
          {product.name}
        </h1>

        <p className="text-2xl text-blue-500 mt-4">
          ${product.price}
        </p>

        <p className="mt-6 text-gray-600">
          {product.description}
        </p>

        <button
          onClick={() => addToCart(product)}
          className="mt-6 bg-green-500 hover:bg-green-600 text-white px-6 py-3 rounded-lg transition"
        >
          Add To Cart
        </button>
      </div>
    </div>
  );
}

export default ProductDetailPage;
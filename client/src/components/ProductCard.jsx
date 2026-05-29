import { Link } from "react-router-dom";

function ProductCard({ product }) {
  return (
    <div className="border rounded-xl p-4 shadow hover:shadow-lg transition overflow-hidden">
      <img
        src={
          product.imageUrl ||
          "https://via.placeholder.com/300"
        }
        alt={product.name}
        className="w-full h-48 object-cover rounded-lg"
      />

      <h2 className="text-xl font-bold mt-3 line-clamp-1">
        {product.name}
      </h2>

      <p className="text-blue-500 font-semibold mt-2">
        ${product.price}
      </p>

      <Link
        to={`/products/${product.id}`}
        className="mt-4 inline-block bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded-lg transition"
      >
        View Detail
      </Link>
    </div>
  );
}

export default ProductCard;
function ProductCard({ product }) {
  return (
    <div className="border rounded-lg p-4 shadow">
      <img
        src={product.imageUrl}
        alt={product.name}
        className="w-full h-48 object-cover rounded"
      />

      <h2 className="text-xl font-bold mt-3">
        {product.name}
      </h2>

      <p className="text-gray-500 mt-2">
        ${product.price}
      </p>

      <button
        className="mt-4 bg-blue-500 text-white px-4 py-2 rounded"
      >
        View Detail
      </button>
    </div>
  );
}

export default ProductCard;
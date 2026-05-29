import { useEffect, useState } from "react";

import ProductCard from "../components/ProductCard";

import { getProducts } from "../services/productService";

function ProductsPage() {
  const [products, setProducts] = useState([]);

  const [loading, setLoading] = useState(false);

  const [error, setError] = useState("");

  const [search, setSearch] = useState("");

  const [page, setPage] = useState(1);

  useEffect(() => {
    fetchProducts();
  }, [page, search]);

  const fetchProducts = async () => {
    try {
      setLoading(true);

      const data = await getProducts(page, 8, search);

      setProducts(data.data || data);
    } catch (err) {
      setError("Failed to fetch products");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-7xl mx-auto p-6">
      <h1 className="text-3xl font-bold mb-6">
        Products
      </h1>

      <input
        type="text"
        placeholder="Search products..."
        value={search}
        onChange={(e) => {
          setSearch(e.target.value);

          setPage(1);
        }}
        className="border p-3 rounded-xl w-full mb-6 shadow"
      />

      {loading && (
        <p className="mb-4">
          Loading...
        </p>
      )}

      {error && (
        <p className="text-red-500 mb-4">
          {error}
        </p>
      )}

      <div className="grid grid-cols-1 md:grid-cols-3 lg:grid-cols-4 gap-6">
        {products.map((product) => (
          <ProductCard
            key={product.id}
            product={product}
          />
        ))}
      </div>

      <div className="flex gap-4 mt-8">
        <button
          onClick={() => setPage(page - 1)}
          disabled={page === 1}
          className="bg-gray-200 px-4 py-2 rounded-xl shadow disabled:opacity-50"
        >
          Prev
        </button>

        <button
          onClick={() => setPage(page + 1)}
          className="bg-gray-200 px-4 py-2 rounded-xl shadow"
        >
          Next
        </button>
      </div>
    </div>
  );
}

export default ProductsPage;
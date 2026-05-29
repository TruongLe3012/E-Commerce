import { useEffect, useState } from "react";

import { Link } from "react-router-dom";

import {
  getProducts,
  deleteProduct,
} from "../services/productService";

function AdminProductsPage() {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = async () => {
    try {
      const data = await getProducts();

      setProducts(data.data || data);
    } catch (error) {
      console.log(error);
    }
  };

  const handleDelete = async (id) => {
    try {
      await deleteProduct(id);

      fetchProducts();
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="max-w-7xl mx-auto p-6">
      <h1 className="text-3xl font-bold mb-6">
        Manage Products
      </h1>

      <Link
        to="/admin/products/create"
        className="inline-block mb-6 bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded-lg transition"
      >
        Create Product
      </Link>

      <table className="w-full border shadow rounded-xl overflow-hidden">
        <thead>
          <tr className="bg-gray-100">
            <th className="p-3">
              Image
            </th>

            <th>Name</th>

            <th>Price</th>

            <th>Actions</th>
          </tr>
        </thead>

        <tbody>
          {products.map((product) => (
            <tr
              key={product.id}
              className="border-t"
            >
              <td className="p-3">
                <img
                  src={product.imageUrl}
                  alt={product.name}
                  className="w-20 h-20 object-cover rounded"
                />
              </td>

              <td>{product.name}</td>

              <td>${product.price}</td>

              <td>
                <button
                  onClick={() =>
                    handleDelete(product.id)
                  }
                  className="bg-red-500 hover:bg-red-600 text-white px-3 py-1 rounded-lg transition"
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default AdminProductsPage;
import { Link } from "react-router-dom";

function AdminDashboardPage() {
  return (
    <div>
      <h1 className="text-4xl font-bold mb-6">
        Admin Dashboard
      </h1>

      <div className="grid md:grid-cols-3 gap-6">
        <Link
          to="/admin/products"
          className="border p-6 rounded-xl shadow hover:bg-gray-100 transition cursor-pointer"
        >
          Products
        </Link>

        <div className="border p-6 rounded-xl shadow">
          Orders
        </div>

        <div className="border p-6 rounded-xl shadow">
          Users
        </div>
      </div>
    </div>
  );
}

export default AdminDashboardPage;
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";

import { createProduct } from "../services/productService";
import { uploadImage } from "../services/uploadService";
import { getCategories } from "../services/categoryService";

function CreateProductPage() {
  const navigate = useNavigate();

  const [loading, setLoading] = useState(false);
  const [uploading, setUploading] = useState(false);
  const [categories, setCategories] = useState([]);

  const [formData, setFormData] = useState({
    name: "",
    price: "",
    description: "",
    imageUrl: "",
    categoryId: 0,
  });

  // ================= FETCH CATEGORY =================
  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    try {
      const data = await getCategories();
      setCategories(data.data || data);
    } catch (error) {
      console.log(error);
    }
  };

  // ================= UPLOAD IMAGE =================
  const handleUpload = async (e) => {
    const file = e.target.files[0];
    if (!file) return;

    try {
      setUploading(true);

      const data = await uploadImage(file);

      const url = data.imageUrl || data.url || data;

      setFormData((prev) => ({
        ...prev,
        imageUrl: url,
      }));
    } catch (error) {
      console.log(error);
      toast.error("Upload failed");
    } finally {
      setUploading(false);
    }
  };

  // ================= CHANGE =================
  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  // ================= SUBMIT =================
  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!formData.name || !formData.price) {
      toast.error("Name and Price are required");
      return;
    }

    if (!formData.categoryId || formData.categoryId === "0") {
      toast.error("Please select category");
      return;
    }

    setLoading(true);

    try {
      const payload = {
        name: formData.name.trim(),
        price: Number(formData.price),
        description: formData.description.trim(),
        imageUrl: formData.imageUrl,
        categoryId: Number(formData.categoryId),
      };

      console.log("SUBMIT:", payload);

      await createProduct(payload);

      toast.success("Product created");

      setFormData({
        name: "",
        price: "",
        description: "",
        imageUrl: "",
        categoryId: 0,
      });

      navigate("/admin/products");
    } catch (error) {
      console.log("CREATE ERROR:", error.response?.data || error);
      toast.error("Create failed");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-3xl mx-auto p-6">
      <h1 className="text-3xl font-bold mb-6">
        Create Product
      </h1>

      <form onSubmit={handleSubmit} className="space-y-4">

        <input
          name="name"
          placeholder="Product Name"
          value={formData.name}
          onChange={handleChange}
          className="w-full border p-3 rounded-lg"
        />

        <input
          type="number"
          name="price"
          placeholder="Price"
          value={formData.price}
          onChange={handleChange}
          className="w-full border p-3 rounded-lg"
        />

        <textarea
          name="description"
          placeholder="Description"
          value={formData.description}
          onChange={handleChange}
          className="w-full border p-3 rounded-lg"
          rows={5}
        />

        {/* UPLOAD */}
        <input
          type="file"
          accept="image/*"
          onChange={handleUpload}
          className="w-full border p-3 rounded-lg"
        />

        {uploading && (
          <p className="text-blue-500">Uploading...</p>
        )}

        {formData.imageUrl && (
          <img
            src={formData.imageUrl}
            className="w-60 h-60 object-cover rounded mt-2"
            alt="preview"
          />
        )}

        {/* CATEGORY */}
        <select
          name="categoryId"
          value={formData.categoryId}
          onChange={handleChange}
          className="w-full border p-3 rounded-lg"
        >
          <option value="0">Select Category</option>

          {categories.map((cat) => (
            <option key={cat.id} value={cat.id}>
              {cat.name}
            </option>
          ))}
        </select>

        <button
          disabled={loading}
          className={`px-6 py-3 rounded-lg text-white ${
            loading
              ? "bg-gray-400"
              : "bg-blue-500 hover:bg-blue-600"
          }`}
        >
          {loading ? "Creating..." : "Create"}
        </button>
      </form>
    </div>
  );
}

export default CreateProductPage;
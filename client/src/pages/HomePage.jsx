import { useEffect } from "react";
import { getProducts } from "../services/productService";

function HomePage() {
  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = async () => {
    try {
      const data = await getProducts();

      console.log(data);
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div>
      <h1 className="text-3xl font-bold">
        Home Page
      </h1>
    </div>
    
  );
}

export default HomePage;
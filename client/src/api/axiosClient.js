import axios from "axios";

const axiosClient = axios.create({
  baseURL: "https://localhost:7010/api",
  headers: {
    "Content-Type": "application/json",
  },
});

export default axiosClient;
import axios from "axios";

export const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_URL ?? "https://localhost:7081/api"
});

apiClient.interceptors.request.use((config) => {
  const token = localStorage.getItem("myfitpeak.jwt");

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

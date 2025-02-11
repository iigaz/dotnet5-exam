class Config {
  static BaseURL = import.meta.env.VITE_API_URL ?? "http://localhost:5288/api/";
}

export default Config;

export default function checkLoggedIn(): Boolean {
  const accessToken = localStorage.getItem("access_token");

  return accessToken !== null;
}

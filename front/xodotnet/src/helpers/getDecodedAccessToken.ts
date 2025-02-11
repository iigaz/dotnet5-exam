// @ts-ignore
export default function getUsernameFromAccessToken(): any {
  try {
    return JSON.parse(
      atob(localStorage.getItem("access_token")!.split(".")[1]),
    )["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
  } catch (Error) {
    return null;
  }
}

import classes from "./userInfo.module.css";
import CustomLoader from "../../general/loader/customLoader.tsx";

function UserInfo(props: UserInfoProps | null) {
  return (
    <div className={classes.userInfoContainer}>
      {props === null ? (
        <CustomLoader></CustomLoader>
      ) : props.rating === undefined || props.username === undefined ? (
        <CustomLoader></CustomLoader>
      ) : (
        <>
          <p>{props!.username}</p>
          <p>{`Рейтинг ${props!.rating}`}</p>
        </>
      )}
    </div>
  );
}

export default UserInfo;

export interface UserInfoProps {
  username: string;
  rating: number;
}

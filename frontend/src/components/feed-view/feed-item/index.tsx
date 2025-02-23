import { styled } from "@mui/system";
import { Card } from "@mui/material";
import FeedItemText from "./feed-item-text";
import FeedItemMedia from "./feed-item-media";
import FeedItemFooter from "./feed-item-footer";

const StyledCard = styled(Card)(({}) => ({
  backgroundColor: "#d5d5d5",
  position: "relative",
  overflow: "visible",
}));

interface FeedItemProps {
  time: string;
  name: string;
  avatar?: string;
  image?: string;
  iframe?: string;
  children: string;
}

const Item = ({
  time,
  name,
  avatar,
  image,
  iframe,
  children,
}: FeedItemProps) => (
  <StyledCard elevation={4}>
    {image && <FeedItemMedia component="img" src={image} />}
    {iframe && <FeedItemMedia component="iframe" src={iframe} />}
    <FeedItemText>{children}</FeedItemText>
    <FeedItemFooter name={name} time={time} avatar={avatar} />
  </StyledCard>
);

export default Item;

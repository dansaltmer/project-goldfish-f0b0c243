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
  media?: FeedMedia;
  children: string;
}

const Item = ({ time, name, avatar, media, children }: FeedItemProps) => (
  <StyledCard elevation={4}>
    {media && (
      <FeedItemMedia
        component={media.type == "image" ? "img" : media.type} // slightly different in my api
        src={media.url}
      />
    )}
    <FeedItemText>{children}</FeedItemText>
    <FeedItemFooter name={name} time={time} avatar={avatar} />
  </StyledCard>
);

export default Item;

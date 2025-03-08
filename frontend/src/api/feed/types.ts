/* eslint-disable @typescript-eslint/no-unused-vars */

interface UserProfile {
  id: string;
  name: string;
  avatar?: string;
}

interface FeedMedia {
  type: string;
  url: string;
}

interface FeedMessage {
  id: string;
  feedId: string;
  timestamp: string;
  profile: UserProfile;
  media?: FeedMedia;
  text: string;
}

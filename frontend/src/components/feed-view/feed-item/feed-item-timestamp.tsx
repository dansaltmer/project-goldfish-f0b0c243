import { useEffect, useState } from "react";

// Seems to be the easier approach than pulling in a package
// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Intl/RelativeTimeFormat/RelativeTimeFormat
const formatter = new Intl.RelativeTimeFormat();

// Take iso8601 timestamp and convert to local relative time display
const getDisplayTime = (timestamp: string) => {
  const seconds = (Date.now() - new Date(timestamp).getTime()) / 1000;
  if (seconds < 60) {
    return formatter.format(-Math.round(seconds), "seconds");
  }

  const minutes = seconds / 60;
  if (minutes < 60) {
    return formatter.format(-Math.round(minutes), "minutes");
  }

  const hours = minutes / 60;
  if (hours < 24) {
    return formatter.format(-Math.round(hours), "hours");
  }

  const days = hours / 24;
  return formatter.format(-Math.round(days), "days");
};

interface FeedItemTimestampProps {
  timestamp: string;
}

const FeedItemTimestamp = ({ timestamp }: FeedItemTimestampProps) => {
  const [display, setDisplay] = useState(getDisplayTime(timestamp));

  // Could be a little more dynamic with the interval
  // duration to be efficient on older dates
  useEffect(() => {
    setDisplay(getDisplayTime(timestamp));

    const timer = setInterval(() => {
      setDisplay(getDisplayTime(timestamp));
    }, 10000);

    return () => clearInterval(timer);
  }, [timestamp]);

  return <>{display}</>;
};

export default FeedItemTimestamp;

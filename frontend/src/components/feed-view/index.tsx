import React from "react";
import FeedItem from "./feed-item";

import { Masonry } from "@mui/lab";

const FeedView = ({}) => (
  <main>
    <Masonry columns={3} spacing={4} sx={{ margin: "2px" }}>
      <FeedItem
        time="25 seconds ago"
        name="Bob S."
        avatar="https://www.gravatar.com/avatar/8aede19bd7a227ec90297ff1be25c1f5?s=48&d=identicon"
        image="https://mui.com/static/images/cards/paella.jpg"
      >
        Curabitur efficitur hendrerit enim, vitae condimentum lectus aliquam ut.
        Phasellus eu dictum nisl, ac eleifend lacus. Maecenas imperdiet orci
        tincidunt eleifend bibendum. Maecenas et interdum lectus. Sed ornare
        eget leo a rhoncus. Cras imperdiet volutpat nulla gravida euismod. Cras
        nulla lorem, gravida et ipsum at, rhoncus feugiat leo. Donec dignissim
        lobortis porta.
      </FeedItem>
      <FeedItem time="25 seconds ago" name="Bob S.">
        Something hereasnd,ma sndsahkdjh sajkhdkja shkdjhs ajkdhajskd hjash
        dkjahskjdhjkashdkjsahdjk haskdhaskjhd
      </FeedItem>
      <FeedItem time="25 seconds ago" name="Dave L.">
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec tellus
        nibh, fringilla ut neque commodo, gravida malesuada leo. Morbi eget
        justo eget tellus ornare ornare non eget tellus. Suspendisse volutpat
        consequat orci, sollicitudin bibendum ex mollis nec. Nulla facilisis
        pharetra pellentesque. Duis ut dui velit. Maecenas diam urna, placerat
        ut quam a, laoreet placerat nibh. Etiam tristique enim libero. Nunc
        varius eget elit quis hendrerit. Morbi posuere pharetra posuere.
        Praesent pharetra eros eget iaculis sollicitudin. Etiam diam lorem,
        congue a quam id, suscipit finibus justo. Curabitur efficitur hendrerit
        enim, vitae condimentum lectus aliquam ut. Phasellus eu dictum nisl, ac
        eleifend lacus. Maecenas imperdiet orci tincidunt eleifend bibendum.
        Maecenas et interdum lectus. Sed ornare eget leo a rhoncus. Cras
        imperdiet volutpat nulla gravida euismod. Cras nulla lorem, gravida et
        ipsum at, rhoncus feugiat leo. Donec dignissim lobortis porta.
      </FeedItem>
      <FeedItem time="25 seconds ago" name="Bob B.">
        Something here asdasd sadasdsad as
      </FeedItem>
      <FeedItem
        time="25 seconds ago"
        name="Tomate"
        avatar="https://yt3.ggpht.com/ytc/AIdro_nSOpvwykBLXxkimRflH3hknIA8MzLARLUK2guBe-JMCQ=s48-c-k-c0x00ffffff-no-rj"
        iframe="https://www.youtube.com/embed/Wk_P6SDkQQ0?si=wduv9K8uK_CQhKfv"
      >
        これはただのランダムなテキストです。
        皆さんお元気ですか？ゼノブレイドをプレイしましたか
      </FeedItem>
      <FeedItem
        time="25 seconds ago"
        name="Tim Tim."
        image="https://mui.com/static/images/cards/paella.jpg"
      >
        Something heremsdahfkjhas dkjasdjkhaskjd hasjkhdjkahd kjas hdkjsah kdj
        ahskjd askj
      </FeedItem>
      <FeedItem time="25 seconds ago" name="Dave S.">
        Something heremsdahfkjhas dkjasdjkhaskjd hasjkhdjkahd kjas hdkjsah kdj
        ahskjd askj
      </FeedItem>
    </Masonry>
  </main>
);

export default FeedView;

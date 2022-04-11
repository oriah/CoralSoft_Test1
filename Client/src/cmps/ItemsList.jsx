import { Item } from "./item";

export function ItemsList({items, fetchMoreData}) {
    return (
        <InfiniteScroll
            dataLength={items.length}
            next={fetchMoreData}
            className="items-list"
            inverse={true} //
            hasMore={true}
            loader={<h4>Loading...</h4>}
        >
            {items.map((item, index) => <Item item={item} key={index}></Item>)}
        </InfiniteScroll>
    )
}
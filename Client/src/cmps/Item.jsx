export function Item({item}) {
    return (
        <div class="item">
            <h5>{item.title}</h5>
            <img src={item.imgUrl}/>
        </div>
    )
}
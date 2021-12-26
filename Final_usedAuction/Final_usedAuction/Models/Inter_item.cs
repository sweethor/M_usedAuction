using System;
using System.Collections.Generic;
using System.Text;

namespace Final_usedAuction.Models
{
    public class Inter_item
    {
        public int Sell_num { get; set; } // 게시글 번호
        public string Sell_name { get; set; } // 게시글 이름
        public string Sell_contents { get; set; } // 내용
        public string Sell_img { get; set; }// 사진
        public int Sell_price { get; set; } // 가격
        public string Sell_ID { get; set; } // 판매자 ID
        public string Topbid_ID { get; set; } //  최고 입찰 ID
        public int Highest_bid { get; set; }  // 최고입찰가
        public int Sell_mode { get; set; } // 판매방식 : 1 = 경매 , 2 = 구매 , 3 = 경매 + 구매
    }
}

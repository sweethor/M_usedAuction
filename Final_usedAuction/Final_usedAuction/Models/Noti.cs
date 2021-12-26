using System;
using System.Collections.Generic;
using System.Text;

namespace Final_usedAuction.Models
{
    class Noti
    {
        public int noti_num { get; set; } // 알림번호
        public string noti { get; set; } // 알림내용
        public string memid { get; set; } // 거래상대id
        public int sellitem_num { get; set; } // 아이템 페이지 번호
        public int page_sw { get; set; } //페이지 선택 스위치 1 : 즐겨찾기 및 경매 중,  2 : 구매완료 , 3 : 판매완료
    }
}

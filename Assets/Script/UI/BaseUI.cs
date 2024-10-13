using UnityEngine.EventSystems;

public class BaseUI : UIBehaviour
{
    // UI의 공통 동작
    protected bool _isOpen;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start(); 
    }

    // UI 활성화 메서드
    public virtual void Show()
    {
        _isOpen = true;
        gameObject.SetActive(true);
        OnShow();
    }

    // UI 비활성화 메서드
    public virtual void Hide()
    {
        _isOpen = false;
        gameObject.SetActive(false);
        OnHide();
    }

    // UI 초기화 (필요한 경우 자식 클래스에서 재정의 가능)
    public virtual void Initialize()
    {
        // 공통 초기화 작업
    }

    // HUD와 Window에서 따로 구현할 메서드
    protected virtual void OnShow() { }
    protected virtual void OnHide() { }

    // UI의 상태를 토글
    public void Toggle()
    {
        if (_isOpen)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
}

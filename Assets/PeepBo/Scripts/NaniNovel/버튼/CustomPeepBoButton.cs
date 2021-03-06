using PeepBo.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* Unity의 Button을 바로 상속받음
 * 버튼 클릭 효과만 추가
 * 기본 UI가 아닌 CustomUI에 대응하기 위해 생성 <-> CustomLabeledButton
 */
public class CustomPeepBoButton : Button
{
	bool isClicked = false;

	RectTransform rectTransform;
	Vector3 originScale;


	protected override void Start()
	{
		base.Start();
		transition = default;
		rectTransform = GetComponent<RectTransform>();
		originScale = rectTransform.localScale;
	}

	private void Update()
	{
		if (isClicked)
			rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, originScale * 1.05f, 0.5f);
		else
			rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, originScale, 0.5f);
	}


	public override void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
			return;
		base.OnPointerDown(eventData);

		isClicked = true;
		GameManager.Audio.PlayClickSound();
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
			return;
		base.OnPointerUp(eventData);

		isClicked = false;
	}
}
